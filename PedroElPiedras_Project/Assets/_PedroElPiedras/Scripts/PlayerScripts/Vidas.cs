using UnityEngine;

public class Vidas : MonoBehaviour
{
    public int vida = 3;
    public int maxVida = 3;
    public int damage;
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vida = maxVida;
    }
    public void Update()
    {
        if (vida <= 0)
        {
            transform.position = playerController.respawn[playerController.respawnNum].position;
        }
    }
    public void Golpeado( int damage)
    {
        vida -= damage; 
    }
    
    
}
