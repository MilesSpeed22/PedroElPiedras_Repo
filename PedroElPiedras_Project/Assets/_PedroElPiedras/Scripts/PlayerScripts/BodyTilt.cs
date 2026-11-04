using UnityEngine;

public class BodyTilt : MonoBehaviour
{
    [Header("References")]
    public Transform spine;
    public Transform player;
    public Rigidbody rb;

    [Header("Settings")]
    public float maxTiltAngle = 15f;
    public float tiltSpeed = 5f;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = spine.localRotation;
    }

    void LateUpdate()
    {
        Vector3 localVelocity = player.InverseTransformDirection(rb.linearVelocity);

        float targetTilt = -localVelocity.x * maxTiltAngle / 10f;

        targetTilt = Mathf.Clamp(targetTilt, -maxTiltAngle, maxTiltAngle);

        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, targetTilt);

        spine.localRotation = Quaternion.Slerp(spine.localRotation, targetRotation, tiltSpeed * Time.deltaTime);
        
    }
}
