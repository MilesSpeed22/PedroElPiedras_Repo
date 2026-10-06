using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    //Piedra
    public GameObject piedra;
    private GameObject piedraInstanciada;
    public Transform piedraPos;
    public bool canPiedra = true;
    public int numPiedra;

    //Respawn
    public Transform[] respawn;
    public int respawnNum = 0;
    //Disparo
    public GameObject bullet;
    public Transform bulletPos;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyTarget;
    bool canDisparar = true;
    
   
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
            PlatformManager.GetComponent<PlatformManager>().InstPlatform();
        }
    }
    public void OnPiedra(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Piedra();
        }
    }
    public void OnDisparo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           Disparo();
        }
    }

    private void Movimiento()
    {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

        Vector3 moveDirection = camForward* moveInput.y + camRight * moveInput.x;
        //Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

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

    public void Piedra()
    {
        
        
        if (canPiedra && numPiedra <= 1)
        {
            numPiedra++;
            piedraInstanciada = Instantiate(piedra, piedraPos.position, Quaternion.identity);
            canPiedra = false;
            StartCoroutine(CanPiedra());
           
        }
        if(numPiedra==2 && canPiedra)
        {
            numPiedra-=2;
            Destroy(piedraInstanciada);
        }
    }
    IEnumerator CanPiedra()
    {
        yield return new WaitForSeconds(1f);
        canPiedra = true;
    }
  

    public void Disparo()
    {
        if(enemyTarget != null && canDisparar)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            canDisparar = false;
            StartCoroutine(DisparoCC());
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
            ActualizarObjetivos();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            ActualizarObjetivos();
        }
        if (other.CompareTag("Dead"))
        {
            transform.position = respawn[respawnNum].position;
        }
    }
    private void ActualizarObjetivos()
    {
        if (enemies.Count == 0)
        {
            enemyTarget = null;
            return;
        }

        float distanciaMasCorta = Mathf.Infinity;
        GameObject enemigoMasCercano = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
                continue;
            float distancia = Vector3.Distance(transform.position, enemy.transform.position);

            if (distancia < distanciaMasCorta)
            {
                distanciaMasCorta = distancia;
                enemigoMasCercano = enemy;
            }
        }
        enemyTarget = enemigoMasCercano;
    }
    IEnumerator DisparoCC()
    {
        yield return new WaitForSeconds(1f);
        canDisparar = true;
    }
}
