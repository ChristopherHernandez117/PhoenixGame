using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player { get; private set; }
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    private Animator animator;
    public float ChaseRange { get; private set; } = 10.0f;
    public float AttackRange { get; private set; } = 4f;
    public int lifePoints = 4;
    public bool canTakeDamage = true;
    public bool alive = true;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        SetAgentInControl(true);
    }
    private void Update()
    {
        if (lifePoints <= 0)
        {
            animator.SetBool("IsDead", true);
            alive = false;
            SetAgentInControl(false);
        }
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }
    public void HitWithSword()
    {
        // if not available to use (still cooling down) just exit
        if (!canTakeDamage) return;
        animator.SetTrigger("GotHit");
        ReactToHit(0.50f);
        canTakeDamage = false;
        StartCoroutine(tookDamageCountdown(3));
    }
    public void HitWithShield()
    {
        // if not available to use (still cooling down) just exit
        if (!canTakeDamage) return;
        animator.SetTrigger("IsDizzy");
        ReactToHit(0.5f);
        canTakeDamage = false;
        StartCoroutine(tookDamageCountdown(3));
    }
    public void ReactToHit(float timer)
    {
        SetAgentInControl(false);

        Vector3 reactionV = transform.position - Player.transform.position;
        reactionV.Normalize();
        reactionV *= 7;
        reactionV.y = reactionV.y + 2;
        rb.AddForce(reactionV, ForceMode.Impulse);
        // rb.AddRelativeForce(Vector3.back * 2, ForceMode.Impulse);
        // rb.AddRelativeForce(Vector3.up * 2, ForceMode.Impulse);
        StartCoroutine(ReturnControlToAgentAfterDelay(timer));

    }
    private IEnumerator tookDamageCountdown(int cooldownDuration)
    {
        int counter = cooldownDuration;
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.1f);
            counter--;
        }
        canTakeDamage = true;
        //Debug.Log("Can take damage");
    }
    void SetAgentInControl(bool agentInControl = true)
    {
        agent.isStopped = !agentInControl;  // if agent not in control, stop it (otherwise is should be on)
        rb.isKinematic = agentInControl;    // if agent in control, turn the rigidbody off (otherwise rb should be on)
    }
    private IEnumerator ReturnControlToAgentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetAgentInControl(true);
    }
    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange - 1.5f);
        */
    }
}
