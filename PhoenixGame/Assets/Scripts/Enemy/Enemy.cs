using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player { get; private set; }
    [SerializeField] public NavMeshAgent agent;
    private Animator animator;
    public float ChaseRange { get; private set; } = 10.0f;
    public float AttackRange { get; private set; } = 3.7f;
    public int lifePoints = 5;
    public bool canTakeDamage = true;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    
    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }
    public void tookDamage()
    {
        // if not available to use (still cooling down) just exit
        if (!canTakeDamage) return;
        canTakeDamage = false;
        Debug.Log("Enemy CANNOT take damage");
        StartCoroutine(tookDamageCountdown(3));
    }
    public void gotHit()
    {
        animator.SetTrigger("GotHit");
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
        Debug.Log("Can take damage");
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
