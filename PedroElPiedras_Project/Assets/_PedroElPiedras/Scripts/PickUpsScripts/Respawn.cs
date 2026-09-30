using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    [SerializeField] int numRespawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().respawnNum = numRespawn;
        }
    }
}
