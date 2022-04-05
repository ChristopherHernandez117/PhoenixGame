using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player { get; private set; }
    [SerializeField] public NavMeshAgent agent;
    public float ChaseRange { get; private set; } = 10.0f;
    public float AttackRange { get; private set; } = 3.7f;
    public int lifePoints = 5;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        
    }
    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
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
