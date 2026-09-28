using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class PlatformInvoque01 : MonoBehaviour
{
   
    public PlatformManager platformManager;
    [SerializeField] int platformNum;
    [SerializeField] int platPosformNum;
    [SerializeField] int numInstanRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformManager.platformNum = platformNum;
            platformManager.platPosformNum = platPosformNum;
            platformManager.numInstanRef = numInstanRef;

            platformManager.instanRef[numInstanRef].SetActive(true);
            platformManager.canInvoke = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformManager.instanRef[numInstanRef].SetActive(false);
            platformManager.canInvoke = false;
        }
    }

   
}


