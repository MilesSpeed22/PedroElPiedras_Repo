using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
      
    }

 
     public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DoubleJump();
            Destroy(gameObject);
        }
    }

}
