using System.Collections.Generic;
using UnityEngine;

public class CamaraParedes : MonoBehaviour
{
    public Transform player;
  
    public float transparetAlpha = 0.3f;

    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Restaurar Materiales
        foreach (var item in originalColors)
        {
            if (item.Key != null)
            {
                Color c = item.Value;
                item.Key.material.color = c;
            }
        }
        originalColors.Clear();

        //Detectar Paredes
        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, dist);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();

                if (rend != null)
                {
                    if (!originalColors.ContainsKey(rend))
                    {
                        originalColors.Add(rend, rend.material.color);
                    }
                    Color c = rend.material.color;
                    c.a = transparetAlpha;//transparencia
                    rend.material.color = c;
                }
            }
           
        }
    }
}


