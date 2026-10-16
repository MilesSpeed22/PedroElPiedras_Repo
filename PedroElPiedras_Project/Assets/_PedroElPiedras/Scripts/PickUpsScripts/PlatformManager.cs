using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platformList;
    public int platformNum;
    public Transform[] platformPos;
    public int platPosformNum;
    public bool canInvoke = false;
    public bool canMove = true;
    public GameObject[] instanRef;
    public int numInstanRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InstPlatform()
    {
        if (canInvoke && canMove)
        {
            //platformList[platformNum].SetActive(true);
            //platformList[platformNum].GetComponent<PlatformDestroy>().DestroyPlatform();
            platformList[platformNum].GetComponent<MovimientoLerp>().IniciarMovimiento();
            //Instantiate(platformList[platformNum], platformPos[platPosformNum].position, Quaternion.identity);
            canMove = false;
            
        }
    }
}
