
using UnityEngine;

public class PlatformInvoque01 : MonoBehaviour
{
    public PlatformManager platformManager;
    public PlayerController playerController;
    [SerializeField] private int platformNum;
    [SerializeField] private int platPosformNum;
    [SerializeField] private int numInstanRef;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (platformManager == null)
            return;

        platformManager.platformNum = platformNum;
        platformManager.platPosformNum = platPosformNum;
        platformManager.numInstanRef = numInstanRef;

        if (platformManager.instanRef != null &&
            numInstanRef >= 0 &&
            numInstanRef < platformManager.instanRef.Length &&
            platformManager.instanRef[numInstanRef] != null)
        {
            if(other.GetComponent<PlayerController>().platformUnlock == true)
            {
                platformManager.instanRef[numInstanRef].SetActive(true);
            }
            
        }

        platformManager.canInvoke = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (platformManager == null)
            return;

        // Desactivamos solo la referencia de este objeto.
        if (platformManager.instanRef != null &&
            numInstanRef >= 0 &&
            numInstanRef < platformManager.instanRef.Length &&
            platformManager.instanRef[numInstanRef] != null)
        {
            platformManager.instanRef[numInstanRef].SetActive(false);
        }

        // No desactivar el poder si otro objeto ya es el seleccionado.
        if (platformManager.platformNum == platformNum &&
            platformManager.platPosformNum == platPosformNum &&
            platformManager.numInstanRef == numInstanRef)
        {
            platformManager.canInvoke = false;
        }
    }
}
