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

    public AudioManager audioManager;

    public void InstPlatform()
    {
        if (!canInvoke)
            return;

        if (platformList == null ||
            platformNum < 0 ||
            platformNum >= platformList.Length)
        {
            Debug.LogWarning("Numero de plataforma no valido.", this);
            return;
        }

        GameObject plataforma = platformList[platformNum];

        if (plataforma == null)
            return;

        MovimientoLerp movimiento =
            plataforma.GetComponent<MovimientoLerp>();

        if (movimiento == null)
        {
            Debug.LogError(
                "La plataforma no tiene el componente MovimientoLerp.",
                plataforma
            );
            return;
        }

        // Cada plataforma comprueba su propio estado.
        movimiento.IniciarMovimiento();

        audioManager.PlaySFX(3);
    }

    public void CanMove()
    {
        // Ya no es necesario habilitar el movimiento
        // desde el objeto interactuable.
    }
}
