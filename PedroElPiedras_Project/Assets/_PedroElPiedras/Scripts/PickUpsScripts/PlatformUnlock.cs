using UnityEngine;

public class PlatformUnlock : MonoBehaviour
{
    public GameObject Platforms;
    
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Platforms.SetActive(true);
            Destroy(gameObject);
        }
    }
}
