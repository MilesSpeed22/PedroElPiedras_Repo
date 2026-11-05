using UnityEngine;

public class PlatformUnlock : MonoBehaviour
{
    
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().platformUnlock = true;
            other.GetComponent<PlayerController>().tutoNum = 0;
            other.GetComponent<PlayerController>().Tutorial();
            Destroy(gameObject);
        }
    }
}
