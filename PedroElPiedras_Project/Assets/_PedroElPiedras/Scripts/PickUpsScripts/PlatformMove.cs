
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
    public bool iniciarAutomaticamente = true;

    private Quaternion rotacionInicial;

    private void Start()
    {
        rotacionInicial = transform.rotation;

        if (iniciarAutomaticamente)
        {
            //IniciarMovimiento();
        }
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
        StartCoroutine(MoverYRotar());
    }

    private IEnumerator MoverYRotar()
    {
        float tiempo = 0f;

        // Colocamos el objeto en el punto inicial
        transform.position = puntoInicio.position;

        Quaternion destinoRotacion =
            Quaternion.Euler(rotacionFinal);

        while (tiempo < duracion)
        {
            float t = tiempo / duracion;

            // Suavizar inicio y final
            float tSuave = Mathf.SmoothStep(0f, 1f, t);

            Vector3 A = puntoInicio.position;
            Vector3 B = puntoIntermedio.position;
            Vector3 C = puntoDestino.position;

            // Curva de Bezier cuadratica
            Vector3 posicion =
                (1f - tSuave) * (1f - tSuave) * A +
                2f * (1f - tSuave) * tSuave * B +
                tSuave * tSuave * C;

            transform.position = posicion;

            // Rotacion suave
            transform.rotation = Quaternion.Slerp(
                rotacionInicial,
                destinoRotacion,
                tSuave
            );

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Asegurar la posicion y rotacion finales
        transform.position = puntoDestino.position;
        transform.rotation = destinoRotacion;
    }
}