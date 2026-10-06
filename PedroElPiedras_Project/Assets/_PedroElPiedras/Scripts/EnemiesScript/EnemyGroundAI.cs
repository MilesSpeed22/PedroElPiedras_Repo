using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGroundAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    private Transform player;
    private bool follow = false;
    bool canAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            agent.SetDestination(player.position);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            follow = true;
            Attack();
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
      
        
        anim.SetBool("Attack", true);
        StartCoroutine(IDLE());
       
    }

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Attack", false);
    }
    IEnumerator CanAttackCC()
    {
        canAttack = true;
        yield return new WaitForSeconds(1);
        canAttack = false;
    }
}
