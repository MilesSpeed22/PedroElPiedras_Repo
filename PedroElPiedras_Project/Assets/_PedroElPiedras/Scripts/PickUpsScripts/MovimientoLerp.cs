

using UnityEngine;
using System.Collections;

public class MovimientoLerp : MonoBehaviour
{
    [Header("Puntos de la trayectoria")]
    public Transform puntoInicio;
    public Transform puntoIntermedio;
    public Transform puntoDestino;

    [Header("Rotacion")]
    public Vector3 rotacionFinal;

    [Header("Configuracion")]
    public float duracion = 2f;
    public float tiempoEspera = 3f;
    public bool iniciarAutomaticamente = false;

    private Quaternion rotacionInicial;

    public PlatformManager manager;

    private TemblorPlataforma temblor;

    [SerializeField] public bool canMove = true;

    // Impide reutilizar esta plataforma.
    [SerializeField] private bool yaActivada = false;

    public bool YaActivada => yaActivada;

    private void Start()
    {
        rotacionInicial = transform.rotation;

        temblor = GetComponent<TemblorPlataforma>();
    }

    public void IniciarMovimiento()
    {
        if (yaActivada || !canMove)
            return;

        if (puntoInicio == null ||
            puntoIntermedio == null ||
            puntoDestino == null)
        {
            Debug.LogError(
                "Asigna los tres puntos de la trayectoria.",
                this
            );
            return;
        }

        // Se bloquea para siempre, aunque el movimiento termine.
        yaActivada = true;
        canMove = false;

        StopAllCoroutines();
        StartCoroutine(CicloMovimiento());

        if (temblor != null)
        {
            temblor.ActivarTemblor();
        }
    }

    private IEnumerator CicloMovimiento()
    {
        rotacionInicial = transform.rotation;

        Quaternion destinoRotacion =
            Quaternion.Euler(rotacionFinal);

        // Colocamos la plataforma en el inicio.
        transform.position = puntoInicio.position;
        
        // Ida: forma el puente.
        yield return StartCoroutine(
            MoverEntrePuntos(
                puntoInicio.position,
                puntoIntermedio.position,
                puntoDestino.position,
                rotacionInicial,
                destinoRotacion
            )
        );

        // Espera en el destino.
        yield return new WaitForSeconds(tiempoEspera);

        

        // Vuelta: regresa al punto inicial.
        yield return StartCoroutine(
            MoverEntrePuntos(
                puntoDestino.position,
                puntoIntermedio.position,
                puntoInicio.position,
                destinoRotacion,
                rotacionInicial
            )
        );

        // La plataforma ya ha regresado: puede activarse otra vez.
        yaActivada = false;
        canMove = true;

    }

    private IEnumerator MoverEntrePuntos(
        Vector3 A,
        Vector3 B,
        Vector3 C,
        Quaternion rotacionDesde,
        Quaternion rotacionHasta)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float t = duracion > 0f
                ? tiempo / duracion
                : 1f;

            float tSuave = Mathf.SmoothStep(0f, 1f, t);

            Vector3 posicion =
                (1f - tSuave) * (1f - tSuave) * A +
                2f * (1f - tSuave) * tSuave * B +
                tSuave * tSuave * C;

            transform.position = posicion;

            transform.rotation = Quaternion.Slerp(
                rotacionDesde,
                rotacionHasta,
                tSuave
            );

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Ajuste exacto de la posición y rotación finales.
        transform.position = C;
        transform.rotation = rotacionHasta;
    }
  

}
