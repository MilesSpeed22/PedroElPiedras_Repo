using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public CharacterController playerController;
    //movimiento
    public float speed = 5f;
    private Vector2 inputMovement = Vector2.zero;
    Vector3 moveDirection;
    Vector3 velocity;
    float gravity = -9.81f;
    //Salto
    public float jumpHeight = 2f;
    private Vector3 verticalVelocity;
    private bool jumpPressed = false;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        //MovementTec();
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
    public void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
    }
    
    public void OnJump(InputValue value)
    {
        
        
        jumpPressed = value.isPressed;
        
    }

    private void Movimiento()
    {
        moveDirection = transform.forward * inputMovement.y + transform.right * inputMovement.x;
        playerController.Move(moveDirection * speed * Time.deltaTime);


        
    }

}
