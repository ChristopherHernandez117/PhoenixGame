using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player { get; private set; }
    [SerializeField] public NavMeshAgent agent;
    public float ChaseRange { get; private set; } = 10.0f;
    private int lifePoints = 5;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            lifePoints--;
            Debug.Log("Life points:" + lifePoints);
        }
        if(lifePoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }
}
