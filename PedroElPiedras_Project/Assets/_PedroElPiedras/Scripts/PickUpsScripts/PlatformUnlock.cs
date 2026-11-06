using UnityEngine;

public class PlatformUnlock : MonoBehaviour
{
    public GameObject bolaDesactivar;
    public GameObject bolaActivar;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().platformUnlock = true;
            other.GetComponent<PlayerController>().tutoNum = 0;
            other.GetComponent<PlayerController>().Tutorial();
            bolaDesactivar.SetActive(false);
            bolaActivar.SetActive(true);
            Destroy(gameObject);
        }
    }
}
