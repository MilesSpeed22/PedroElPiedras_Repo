using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] int numMusic;

    [Header("AudioClipManager")]
    public AudioClip[] musicList;
    public AudioClip[] SFXList;
    private void Start()
    {
        PlayMusic(numMusic);
    }


    public void PlayMusic(int musicIndex)
    {
        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    }

    public void PlaySFX(int SFXIndex)
    {
        SFXSource.clip = SFXList[SFXIndex];
        SFXSource.Play();
    }
}
