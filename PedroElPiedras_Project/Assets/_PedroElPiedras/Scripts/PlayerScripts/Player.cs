using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController playerController;
    public float speed = 5f;
    Vector3 velocity;
    float gravity = -9.81f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementTec();
    }
    void MovementTec()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            playerController.Move(direction * speed * Time.deltaTime);
        }
        velocity.y += gravity + Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);


    }
}
