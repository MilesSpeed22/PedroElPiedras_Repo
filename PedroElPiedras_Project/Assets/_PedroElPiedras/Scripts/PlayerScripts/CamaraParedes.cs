using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Hace transparentes los objetos que se interponen entre la cámara y el jugador.
// Los materiales de esos objetos deben ser OPACOS: el script les pone una copia
// transparente solo mientras tapan al jugador y después les devuelve el original.
// Pensado para URP con el shader Lit.
public class CamaraParedes : MonoBehaviour
{
    public Transform player;

    [Tooltip("Altura sobre el pivote del jugador a la que apunta la detección (pecho, no pies).")]
    public float targetHeight = 1f;

    [Tooltip("Grosor de la detección. 0 = rayo fino.")]
    public float radius = 0.3f;

    [Tooltip("Capas de los objetos que pueden tapar al jugador (sin incluir al jugador ni el suelo).")]
    public LayerMask occluderLayers;

    [Range(0f, 1f)]
    public float transparentAlpha = 0.3f;

    // Objetos que ahora mismo están tapando -> sus materiales originales
    private readonly Dictionary<Renderer, Material[]> occluding = new Dictionary<Renderer, Material[]>();
    // Material original -> su copia transparente (se crea una sola vez y se reutiliza)
    private readonly Dictionary<Material, Material> transparentCopies = new Dictionary<Material, Material>();
    private readonly HashSet<Renderer> hitThisFrame = new HashSet<Renderer>();
    private readonly List<Renderer> toRestore = new List<Renderer>();

    // LateUpdate: así la detección usa la posición de la cámara ya actualizada
    void LateUpdate()
    {
        if (player == null) return;

        hitThisFrame.Clear();

        Vector3 origin = transform.position;
        Vector3 target = player.position + Vector3.up * targetHeight;
        Vector3 direction = target - origin;
        float distance = direction.magnitude;

        if (distance > 0.01f)
        {
            RaycastHit[] hits = Physics.SphereCastAll(origin, radius, direction / distance, distance,
                                                      occluderLayers, QueryTriggerInteraction.Ignore);

            foreach (RaycastHit hit in hits)
            {
                // GetComponentInChildren también encuentra el Renderer si el mesh está en un hijo
                Renderer rend = hit.collider.GetComponentInChildren<Renderer>();
                if (rend == null) continue;

                hitThisFrame.Add(rend);

                // Solo se cambia el material cuando EMPIEZA a tapar, no en cada frame
                if (!occluding.ContainsKey(rend))
                {
                    Material[] originalMaterials = rend.sharedMaterials;
                    occluding.Add(rend, originalMaterials);

                    Material[] transparentMaterials = new Material[originalMaterials.Length];
                    for (int i = 0; i < originalMaterials.Length; i++)
                        transparentMaterials[i] = GetTransparentCopy(originalMaterials[i]);

                    rend.sharedMaterials = transparentMaterials;
                }
            }
        }

        // Los que tapaban antes y ya no, vuelven a su material opaco
        toRestore.Clear();
        foreach (Renderer rend in occluding.Keys)
            if (!hitThisFrame.Contains(rend))
                toRestore.Add(rend);

        foreach (Renderer rend in toRestore)
        {
            if (rend != null) rend.sharedMaterials = occluding[rend];
            occluding.Remove(rend);
        }
    }

    Material GetTransparentCopy(Material original)
    {
        if (original == null) return null;
        if (transparentCopies.TryGetValue(original, out Material copy)) return copy;

        copy = new Material(original);
        copy.name = original.name + " (Transparente)";

        // Equivale a poner Surface Type: Transparent y Blending Mode: Alpha en el inspector de URP Lit
        copy.SetFloat("_Surface", 1f);
        copy.SetFloat("_Blend", 0f);
        copy.SetOverrideTag("RenderType", "Transparent");
        copy.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
        copy.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
        copy.SetFloat("_SrcBlendAlpha", (float)BlendMode.One);
        copy.SetFloat("_DstBlendAlpha", (float)BlendMode.OneMinusSrcAlpha);
        copy.SetFloat("_ZWrite", 0f);
        copy.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        copy.renderQueue = (int)RenderQueue.Transparent;

        Color color = copy.color;
        color.a = transparentAlpha;
        copy.color = color;

        transparentCopies.Add(original, copy);
        return copy;
    }

    void OnDisable()
    {
        // Deja todo como estaba si se desactiva el script o se cambia de escena
        foreach (var entry in occluding)
            if (entry.Key != null) entry.Key.sharedMaterials = entry.Value;
        occluding.Clear();
    }

    void OnDestroy()
    {
        foreach (Material copy in transparentCopies.Values)
            Destroy(copy);
    }
}
