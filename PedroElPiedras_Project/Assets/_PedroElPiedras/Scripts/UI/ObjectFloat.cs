using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    [Header("Movimiento")]
    public float amplitud = 5f;
    public float velocidad = 1.2f;
    public float desfase = 0f;

    [Tooltip("Invierte la dirección del movimiento y el balanceo (Efecto Espejo)")]
    public bool invertirMovimiento = false; // <-- NUEVO: Casilla para activar el espejo

    [Header("Balanceo")]
    [Tooltip("Balanceo izquierda / derecha (Eje Z)")]
    public float balanceoZ = 2f;
    [Tooltip("Balanceo adelante / atrás (Eje X)")]
    public float balanceoX = 4f;

    RectTransform rt;
    Vector2 posUI;
    Vector3 pos3D;
    Quaternion rotInicial;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        rotInicial = transform.localRotation;

        if (rt != null) posUI = rt.anchoredPosition;
        else pos3D = transform.localPosition;
    }

    void Update()
    {
        // Si la casilla está marcada, sumamos PI (3.1416) para invertir las ondas
        float desfaseFinal = desfase + (invertirMovimiento ? Mathf.PI : 0f);

        float ondaSino = Mathf.Sin(Time.time * velocidad + desfaseFinal);
        float ondaCoso = Mathf.Cos(Time.time * velocidad + desfaseFinal);

        // Posición
        if (rt != null)
        {
            rt.anchoredPosition = posUI + Vector2.up * ondaSino * amplitud;
        }
        else
        {
            transform.localPosition = pos3D + Vector3.up * ondaSino * amplitud;
        }

        // Rotación combinada
        float rotX = ondaCoso * balanceoX;
        float rotZ = ondaSino * balanceoZ;

        // Si está invertido, también multiplicamos por -1 las rotaciones para el espejo total
        if (invertirMovimiento)
        {
            rotX *= -1f;
            rotZ *= -1f;
        }

        transform.localRotation = rotInicial * Quaternion.Euler(rotX, 0f, rotZ);
    }
}
