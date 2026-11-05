using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
    public float fuerzaEmpuje = 50f;
 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            collision.gameObject.GetComponent<Vidas>().Golpeado(1);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (player != null)
            {
                Vector3 direccion = (collision.transform.position - transform.position).normalized;

                direccion.y = 0f;
                direccion.Normalize();
                player.RecibirEmpuje(direccion * fuerzaEmpuje);
                Debug.Log("Empujando a: " + collision.gameObject.name);
            }
        }
    }
}
