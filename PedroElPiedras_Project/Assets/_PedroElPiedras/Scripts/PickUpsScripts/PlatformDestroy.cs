using System;
using UnityEngine;
using System.Collections;

public class PlatformDestroy : MonoBehaviour
{
    [SerializeField] private float numDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyPlatform()
    {
        StartCoroutine(TimeDestroy());
    }

    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(numDestroy);
        gameObject.SetActive(false);
    }
}
