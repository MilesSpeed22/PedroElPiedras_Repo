using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject panelControles;
    public GameObject panelSettings;
    public GameObject canva;

    private void Start()
    {
        canva.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }
    public void NewGame()
    {

        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Seliendo del juego");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit()

#endif

    }

    
    public void OpenControls()
    {
        panelControles.SetActive(true);
        panelControles.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }

    public void CloseControls()
    {
        panelControles.SetActive(false);
        canva.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }

    
    public void OpenSettings()
    {
        panelSettings.SetActive(true);
        panelSettings.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }

    public void CloseSettings()
    {
        panelSettings.SetActive(false);
        canva.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }
}


