using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
    public float fuerzaEmpuje = 50f;
    public Vidas vidas;
    public PlayerController playerController;
    public GameObject player;

    [Header("Puntos del recorrido")]
    public Transform[] puntos = new Transform[4];

    [Header("Configuración")]
    public float duracion = 2f;

    private int puntoActual = 0;
    private float tiempo = 0f;

    private Vector3 posicionInicial;
    void Start()
    {
        if (puntos.Length < 4)
        {
            Debug.LogError("Necesitas asignar 4 puntos");
            enabled = false;
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (puntos[i] == null)
            {
                Debug.LogError("Falta asignar el punto " + i);
                enabled = false;
                return;
            }
        }

        transform.position = puntos[0].position;
        posicionInicial = puntos[0].position;
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        float t = Mathf.Clamp01(tiempo / duracion);

        transform.position = Vector3.Lerp(
            posicionInicial,
            puntos[puntoActual].position,
            t
        );

        if (t >= 1f)
        {
            posicionInicial = puntos[puntoActual].position;

            puntoActual = (puntoActual + 1) % 4;

            tiempo = 0f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Vidas playerVidas = collision.gameObject.GetComponent<Vidas>();
            playerVidas.Golpeado(1);

            if (vidas.vida > 0)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();

                if (player != null)
                {
                    Vector3 direccion = (player.transform.position - transform.position).normalized;

                    direccion.y = 0f;
                    direccion.Normalize();
                    //player.RecibirEmpuje(direccion * fuerzaEmpuje);
                    playerController.puedeMoverse = false;
                    playerController.MoverseCorrutina();
                    rb.AddForce(direccion * fuerzaEmpuje, ForceMode.Impulse);
                    Debug.Log("Empujando a: " + player.name);
                }
            }
            else Debug.Log("El player ha muerto");

        
       
        }
    }
   
}
