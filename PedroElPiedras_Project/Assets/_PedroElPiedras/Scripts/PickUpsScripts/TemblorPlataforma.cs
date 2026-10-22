using System.Collections;
using UnityEngine;

public class TemblorPlataforma : MonoBehaviour
{
  
   
    [Header("Temblor")]
    public float tiempoReposo = 3f;
    public float duracionTemblor = 2f;
    public float intensidadTemblor = 0.05f;
    public float velocidadTemblor = 25f;

    private Vector3 desplazamientoAnterior = Vector3.zero;
    private Coroutine rutinaTemblor;
  
    public void ActivarTemblor()
    {
        if (rutinaTemblor != null) return;

        

        rutinaTemblor = StartCoroutine(CicloTemblor());
    }
   IEnumerator CicloTemblor()
    {
        yield return new WaitForSeconds(tiempoReposo);

        
        float tiempo = 0f;

        while(tiempo < duracionTemblor)
        {
            transform.localPosition -= desplazamientoAnterior;

            float x = Mathf.PerlinNoise(Time.time * velocidadTemblor, 0f) - 0.5f;
            float y = Mathf.PerlinNoise(0f, Time.time * velocidadTemblor) - 0.5f;
            float z = Mathf.PerlinNoise(Time.time * velocidadTemblor, Time.time * velocidadTemblor) - 0.5f;

            desplazamientoAnterior = new Vector3(x, y, z) * intensidadTemblor;

            transform.localPosition += desplazamientoAnterior;

            tiempo += Time.deltaTime;

            yield return null;
        }

        transform.localPosition -= desplazamientoAnterior;
        desplazamientoAnterior = Vector3.zero;
        rutinaTemblor = null;
    }
}
