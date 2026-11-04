using UnityEngine;

public class AudioPlataforma : MonoBehaviour
{

    [SerializeField] AudioSource SFXSource;

    public AudioClip[] SFXList;
   

    public void PlaySFX(int SFXIndex)
    {
        SFXSource.clip = SFXList[SFXIndex];
        SFXSource.Play();
    }
}
