using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour
{
    public float vida = 3f;
    public float maxVida = 3f;
    public float damage;
    public Image barraVida;
    public float damageRate = 0.5f;
    public float damageRateTime = 0f;
    public PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
   
    public void Golpeado( float damage)
    {
        if(Time.time > damageRateTime)
        {
            vida -= damage;
            vida = Mathf.Clamp(vida, 0, maxVida);
            ActualizarBarraVida();
            if (vida <= 0)
            {
                transform.position = playerController.respawn[playerController.respawnNum].position;
                StartCoroutine(DelayVida());
            }
            damageRateTime = Time.time + damageRate;
        }
        
    }

    public void ActualizarBarraVida()
    {
        barraVida.fillAmount = vida / maxVida;
    }
    IEnumerator DelayVida()
    {
        yield return new WaitForSeconds(0.01f);
        vida = maxVida;

    }
    
    
}
