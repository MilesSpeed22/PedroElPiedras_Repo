using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platformList;
    public int platformNum;
    public Transform[] platformPos;
    public int platPosformNum;
    public bool canInvoke = false;
    public GameObject[] instanRef;
    public int numInstanRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InstPlatform()
    {
        if (canInvoke)
        {
            Instantiate(platformList[platformNum], platformPos[platPosformNum].position, Quaternion.identity);
        }
    }
}
