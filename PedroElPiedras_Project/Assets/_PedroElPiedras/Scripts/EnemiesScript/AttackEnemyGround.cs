using UnityEngine;

public class AttackEnemyGround : MonoBehaviour
{
    public float fuerzaEmpuje = 80f;
    public Vidas vidas;
    public PlayerController playerController;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("golpea");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Vidas playerVidas = collision.gameObject.GetComponent<Vidas>();
            playerVidas.Golpeado(1);

            if (vidas.vida > 0)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                Debug.Log("golpea");
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
