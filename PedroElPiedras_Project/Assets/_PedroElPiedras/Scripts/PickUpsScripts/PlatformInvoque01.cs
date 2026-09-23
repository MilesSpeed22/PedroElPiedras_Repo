using UnityEngine;

public class PlatformInvoque01 : MonoBehaviour
{
    public GameObject platform;
    public Transform platformPos;
    public bool canInvoke = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
           
                Instantiate(platform, platformPos.position, Quaternion.identity);
                Destroy(platform,5f);
            
        }
    }

}
