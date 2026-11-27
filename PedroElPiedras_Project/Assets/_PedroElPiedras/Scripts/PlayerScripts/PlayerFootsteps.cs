using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public ParticleSystem rightFootParticles;

    public void RightFoot()
    {
        rightFootParticles.Play();
    }
}
