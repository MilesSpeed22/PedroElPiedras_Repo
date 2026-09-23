using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
      
    }

 
     public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().DoubleJump();
            Destroy(gameObject);
        }
    }

}
