using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class PlatformInvoque01 : MonoBehaviour
{
    public GameObject[] platformList;
    [SerializeField] private int platformNum;
    public Transform[] platformPos;
    [SerializeField] private int platPosformNum;
    public bool canInvoke = false;
    public GameObject[] instanRef;
    public int numInstanRef;
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
            instanRef[numInstanRef].SetActive(true);
            canInvoke = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instanRef[numInstanRef].SetActive(false);
            canInvoke=false;
        }
    }

    public void InstPlatform()
    {
        if (canInvoke)
        {
            Instantiate(platformList[platformNum], platformPos[platPosformNum].position, Quaternion.identity);
        }
    }
}


