using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeleccionarPrimerBoton : MonoBehaviour
{
    [SerializeField] private GameObject primerBoton;
   
    public void SeleccionarBoton()
    {
        StartCoroutine(SeleccionarCuandoEsteActivo());
    }

    IEnumerator SeleccionarCuandoEsteActivo()
    {
        yield return null;

        if (primerBoton != null && primerBoton.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(primerBoton);
        }
    }
}
