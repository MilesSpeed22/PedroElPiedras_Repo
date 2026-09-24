using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    //movimiento
    public float speed = 10f;
    public float rotationSpeed = 6f;
    private Vector2 inputMovement = Vector2.zero;
    Vector3 moveDirection;
    Vector3 velocity;
    Vector2 moveInput;
    //Salto
    public float jumpHeight = 2f;
    public Transform groundCheck;
    private float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;
    public bool doubleJump = false;
    //Dash
    public float dashSpeed = 20f;
    public float timeDash;
    public bool canDash = true;
    //Platform
    public GameObject PlatformManager;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            doubleJump = false;
        }

    }
    private void FixedUpdate()
    {
        Movimiento();
    }
    
  

    public void OnMove(InputAction.CallbackContext context) { moveInput = context.ReadValue<Vector2>(); }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            Jump();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dash();
        }
    }
    public void OnPlatform(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlatformManager.GetComponent<PlatformInvoque01>().InstPlatform();
        }
    }

    private void Movimiento()
    {
        
            Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

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

                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
           
       


    }
    private void Jump()
    {
        if (isGrounded || doubleJump)
        {
           rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            doubleJump = false; 
        }
       
       
    }
    public void DoubleJump()
    {
        doubleJump = true;
        
    }

    public void Dash()
    {
        if (canDash)
        {
            speed = dashSpeed;
            StartCoroutine(DashCC());
            canDash = false;
            StartCoroutine(CanDashCC());
        }
        
    }
    IEnumerator DashCC()
    {
        yield return new WaitForSeconds(0.3f);
        speed = 10f;
    }    
    IEnumerator CanDashCC()
    {
        yield return new WaitForSeconds(timeDash);
        canDash = true;
    }

}
