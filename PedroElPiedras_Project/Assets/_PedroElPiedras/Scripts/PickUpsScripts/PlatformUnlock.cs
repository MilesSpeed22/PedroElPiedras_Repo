using UnityEngine;

public class PlatformUnlock : MonoBehaviour
{
    
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().platformUnlock = true;
            Destroy(gameObject);
        }
    }
}
