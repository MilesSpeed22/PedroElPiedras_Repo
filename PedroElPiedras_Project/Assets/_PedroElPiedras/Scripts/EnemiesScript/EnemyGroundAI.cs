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
    public float dashForce = 15f;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
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
        follow = false;
        attackCol.SetActive(true);
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
  
    }

    public void ResetAttack()
    {
        agent.ResetPath();
        //attackCol.SetActive(false);
    }
    IEnumerator CanAttackCC()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Walk", false);
        anim.SetTrigger("Attack");
    }
}
