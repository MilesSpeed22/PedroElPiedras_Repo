using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class MeteorLifetime : MonoBehaviour
{
    public VisualEffect meteorVFX;

    public float fallDuration = 1f;
    public float impactDuration = 1f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(fallDuration);

        meteorVFX.SendEvent("OnImpact");

        yield return new WaitForSeconds(impactDuration);

        meteorVFX.Stop();

        yield return new WaitForSeconds(0.1f);

        //gameObject.SetActive(false);
    }
}
