using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    public AudioManager audioManager;
    Rigidbody rb;
    //movimiento (si cambio la speed cambiar tambien la funcion del dashCC())
    public float speed = 15f;
    public float rotationSpeed = 10f;
    private Vector2 inputMovement = Vector2.zero;
    Vector3 moveDirection;
    Vector3 velocity;
    Vector2 moveInput;

    [SerializeField] private float acceleration = 32f;
    [SerializeField] private float deceleration = 70f;
    //Salto
    public float jumpHeight = 2f;
    public Transform groundCheck;
    private float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;
    public bool doubleJump = false;
    //Dash
    public float dashSpeed = 30f;
    public float timeDash;
    public bool canDash = true;
    //Platform
    public GameObject PlatformManager;
    public bool platformUnlock;
    

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

    //Tutoriales

    public GameObject[] tutoriales;
    public int tutoNum;

    //MenuPausa

    public GameObject menuPausa;
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
            if(platformUnlock == true)
            {
                PlatformManager.GetComponent<PlatformManager>().InstPlatform();
            }
            
        }
    }
    public void OnPiedra(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Piedra();
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
        if (Camera.main == null) return;

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection =
            camForward * moveInput.y +
            camRight * moveInput.x;

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // Velocidad horizontal actual
        Vector3 velocity = rb.linearVelocity;

        Vector3 currentHorizontalVelocity =
            new Vector3(velocity.x, 0f, velocity.z);

        // Velocidad horizontal deseada
        Vector3 targetVelocity = moveDirection * speed;

        // Aceleración al moverse y desaceleración al soltar el input
        float rate = moveDirection.sqrMagnitude > 0.01f
            ? acceleration
            : deceleration;

        Vector3 newHorizontalVelocity = Vector3.MoveTowards(
            currentHorizontalVelocity,
            targetVelocity,
            rate * Time.fixedDeltaTime
        );

        // Aplicar la nueva velocidad sin alterar la velocidad vertical
        velocity.x = newHorizontalVelocity.x;
        velocity.z = newHorizontalVelocity.z;

        rb.linearVelocity = velocity;

        // Girar hacia donde se mueve el personaje
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }
    private void Jump()
    {
        if (isGrounded || doubleJump)
        {
           rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            doubleJump = false;
            audioManager.PlaySFX(0);
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
            audioManager.PlaySFX(1);
        }
        
    }
    IEnumerator DashCC()
    {
        yield return new WaitForSeconds(0.3f);
        speed = 15f;
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

    //Tutoriales

    public void OnCerrar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CerrarTutorial();
        }
    }

    public void Tutorial()
    {
        tutoriales[tutoNum].SetActive(true);
        playerInput.SwitchCurrentActionMap("Tutorial");
    }

    public void CerrarTutorial()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
        tutoriales[tutoNum].SetActive(false);
    }

    //Menu Pausa
    
    public void OnPausa(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Pausa();
        }
    }

    public void OnCerrarMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CerrarMenu();
        }
    }
    public void Pausa()
    {
        playerInput.SwitchCurrentActionMap("Menu");
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        menuPausa.GetComponent<SeleccionarPrimerBoton>().SeleccionarBoton();
    }

    public void CerrarMenu()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
        menuPausa.SetActive(false);
        Time.timeScale = 1f;

    }
    
    
}
