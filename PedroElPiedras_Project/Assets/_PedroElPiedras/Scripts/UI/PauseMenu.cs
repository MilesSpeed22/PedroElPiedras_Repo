using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;

    public GameObject menuPausa;
    public void Reanudar()
    {
        Time.timeScale = 1.0f;
        menuPausa.SetActive(false);
        player.GetComponent<PlayerController>().playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void MenuInicio()
    {

    }

    public void Opciones()
    {

    }
    public void Salir()
    {
        Application.Quit();
    }
}
