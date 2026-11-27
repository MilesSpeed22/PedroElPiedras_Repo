using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGroundAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    Rigidbody rb;
    private Transform player;
    private bool follow = false;
    bool canAttack;
    public GameObject attackCol;
    public float dashForce = 30f;
    public float dashDistance = 10f;
    private Vector3 startPosition;
    private bool isDashing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            agent.SetDestination(player.position);
            anim.SetBool("Walk", true);
        }
        
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            float distance = Vector3.Distance(startPosition, rb.position);

            if (distance >= dashDistance)
            {
                rb.linearVelocity = Vector3.zero;
                isDashing = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isDashing)
        {
            follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            follow = false;
            agent.ResetPath();
        }
    }

    public void Attack()
    {
        if (isDashing) return;

        follow = false;
        attackCol.SetActive(true);
        isDashing = true;
       

        //guardar la direccion hacia la que mira
        Vector3 direction = transform.forward;
        //desactivar navmesh
        agent.enabled = false;
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

        startPosition = rb.position;
        
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
  
    }

    public void ResetAttack()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        agent.enabled = true;
        agent.Warp(transform.position);
        agent.ResetPath();
        attackCol.SetActive(false);
    }
    IEnumerator CanAttackCC()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Walk", false);
        anim.SetTrigger("Attack");
    }
}
