using System;
using UnityEngine;

public class Tutoriales : MonoBehaviour
{

    public GameObject[] tutoriales;
    [SerializeField] int tutoNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    public void Tutorial()
    {
        tutoriales[tutoNum].SetActive(true);
    }

    public void CerrarTutorial()
    {
        tutoriales[tutoNum].SetActive(false);
    }
}
