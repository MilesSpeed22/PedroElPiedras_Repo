using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;

    public GameObject menuPausa;

    public GameObject opcionesMenu;
    public GameObject controlesMenu;
    public void Reanudar()
    {
        Time.timeScale = 1.0f;
        menuPausa.SetActive(false);
        player.GetComponent<PlayerController>().playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void MenuInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void Opciones()
    {
        opcionesMenu.SetActive(true);
        opcionesMenu.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }
    public void Salir()
    {
        Application.Quit();
    }

    public void VolverOpciones()
    {
        opcionesMenu.SetActive(false);
        menuPausa.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }
    public void VolverControles()
    {
        controlesMenu.SetActive(false);
        opcionesMenu.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }

    public void Controles()
    {
        controlesMenu.SetActive(true);
        controlesMenu.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }
}
