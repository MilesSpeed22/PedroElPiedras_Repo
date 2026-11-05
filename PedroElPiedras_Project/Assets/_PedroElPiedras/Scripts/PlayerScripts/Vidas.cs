using UnityEngine;

public class Vidas : MonoBehaviour
{
    public int vida = 3;
    public int maxVida = 3;
    public int damage;

    public float damageRate = 0.5f;
    public float damageRateTime = 0f;
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vida = maxVida;
    }
   
    public void Golpeado( int damage)
    {
        if(Time.time > damageRateTime)
        {
            vida -= damage;
            if (vida <= 0)
            {
                transform.position = playerController.respawn[playerController.respawnNum].position;
                vida = maxVida;
            }
            damageRateTime = Time.time + damageRate;
        }
        
    }
    
    
}
