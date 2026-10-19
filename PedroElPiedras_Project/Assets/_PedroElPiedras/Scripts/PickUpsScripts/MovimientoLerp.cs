

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

    private void Start()
    {
        rotacionInicial = transform.rotation;

    }

    public void IniciarMovimiento()
    {
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

        StopAllCoroutines();
        StartCoroutine(CicloMovimiento());
    }

    private IEnumerator CicloMovimiento()
    {
        rotacionInicial = transform.rotation;

        Quaternion destinoRotacion =
            Quaternion.Euler(rotacionFinal);

        // Colocamos el objeto en el inicio
        transform.position = puntoInicio.position;

        // Ida
        yield return StartCoroutine(MoverEntrePuntos(puntoInicio.position, puntoIntermedio.position, puntoDestino.position,rotacionInicial,destinoRotacion));

        // Esperamos en el destino
        yield return new WaitForSeconds(tiempoEspera);

        // Vuelta: invertimos la curva
        yield return StartCoroutine(
            MoverEntrePuntos(
                puntoDestino.position,
                puntoIntermedio.position,
                puntoInicio.position,
                destinoRotacion,
                rotacionInicial
            )
        );

        //Tiempo volver a mover
        yield return StartCoroutine(CanMove());
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
            float t = tiempo / duracion;

            // Suavizamos el movimiento
            float tSuave = Mathf.SmoothStep(0f, 1f, t);

            // Curva de Bezier cuadratica
            Vector3 posicion =
                (1f - tSuave) * (1f - tSuave) * A +
                2f * (1f - tSuave) * tSuave * B +
                tSuave * tSuave * C;

            transform.position = posicion;

            // Interpolamos la rotacion
            transform.rotation = Quaternion.Slerp(
                rotacionDesde,
                rotacionHasta,
                tSuave
            );

            tiempo += Time.deltaTime;
            yield return null;

            
        }

        // Ajustamos los valores finales exactos
        transform.position = C;
        transform.rotation = rotacionHasta;
    }

    IEnumerator CanMove()
    {
        yield return new WaitForSeconds(0.1f);

        manager.canMove = true;
    }
}