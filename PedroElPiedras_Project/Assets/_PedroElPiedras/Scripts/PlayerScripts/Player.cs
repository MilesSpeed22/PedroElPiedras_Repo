using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    public CharacterController playerController;
    public Rigidbody rb;
    //movimiento
    public float speed = 5f;
    public float rotationSpeed = 6f;
    private Vector2 inputMovement = Vector2.zero;
    Vector3 moveDirection;
    Vector3 velocity;
    float gravity = -9.81f;
    Vector2 moveInput;
    //Salto
    public float jumpHeight = 2f;
    private Vector3 verticalVelocity;
    private bool jumpPressed = false;
    public Transform groundCheck;
    private float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

       

        //MovementTec();
    }
    private void FixedUpdate()
    {
        Movimiento();
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
    /*
    public void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>();
    }
    */
    public void OnMove(InputAction.CallbackContext context) { moveInput = context.ReadValue<Vector2>(); }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            Jump();
        }
    }

    private void Movimiento()
    {

        Vector3 moveDirection = new Vector3( moveInput.x,0f,moveInput.y );

        if (moveDirection.sqrMagnitude > 1f) moveDirection.Normalize();

        // Movimiento horizontal
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveDirection.x * speed;
        velocity.z = moveDirection.z * speed;

        rb.linearVelocity = velocity;

        // Girar hacia donde se mueve
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime) );
        }


    }
    private void Jump()
    {
        if (isGrounded)
        {
           rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

}
