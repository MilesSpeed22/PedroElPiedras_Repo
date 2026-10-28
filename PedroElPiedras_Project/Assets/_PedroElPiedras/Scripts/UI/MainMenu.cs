using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void NewGame()
    {

        SceneManager.LoadScene(1);
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

    public GameObject panelControles;
    public void OpenControls()
    {
        panelControles.SetActive(true);
    }

    public void CloseControls()
    {
        panelControles.SetActive(false);
    }

    public GameObject panelSettings;
    public void OpenSettings()
    {
        panelSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        panelSettings.SetActive(false);
    }
}


