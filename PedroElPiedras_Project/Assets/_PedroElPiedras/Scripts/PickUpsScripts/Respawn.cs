using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Respawn : MonoBehaviour
{
    //public GameObject refRespawn;
    [SerializeField] int numRespawn;
    Material mat;
    Renderer rend;
    [SerializeField] float lerpTime = 1.5f;
    [SerializeField] Material emissiveMat;
    [SerializeField] Material baseMat;
    Color emColor;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnAwake()
    {
        rend = GetComponentInChildren < Renderer>();
        mat = baseMat;
        emColor = new Color(191f / 255f, 168f / 255f, 66f / 255f, 1f);
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().respawnNum = numRespawn;
            //refRespawn.SetActive(true);
            //mat.EnableKeyword("_EMISSION");
            
            LerpEmissionColor();
        }
    }
    
    private void LerpEmissionColor()
    {
        mat = emissiveMat;
        Color blackColor;

        blackColor = Color.black;
        mat.SetColor("_EmissionColor", blackColor);
        mat.SetColor("_EmissionColor", Color.Lerp(blackColor, emColor,lerpTime));
    }
}
