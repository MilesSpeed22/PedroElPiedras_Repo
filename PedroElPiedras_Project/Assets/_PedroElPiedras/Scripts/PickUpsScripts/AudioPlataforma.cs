using UnityEngine;

public class AudioPlataforma : MonoBehaviour
{

    [SerializeField] AudioSource SFXSource;

    public AudioClip[] SFXList;
   

    public void PlaySFX(int SFXIndex)
    {
        Debug.Log("Entra sonido Manager");
        SFXSource.PlayOneShot(SFXList[SFXIndex]);
    }
}
