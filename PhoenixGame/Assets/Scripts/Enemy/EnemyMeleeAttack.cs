using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private int enemyMeleeDamage = 1;
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void OnTriggerEnter(Collider other)
    {
        HitPlayer(other);
    }
    private void HitPlayer(Collider other)
    {
        if (enemy.alive)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player.canTakeDamage && !player.isDefending)
                {
                    player.TookDamage();
                    player.lifePoints -= enemyMeleeDamage;
                    Debug.Log("Player Life points:" + player.lifePoints);
                }
            }
        }
    }
}
