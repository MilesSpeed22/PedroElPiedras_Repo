using System;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    [SerializeField] private float numDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, numDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
