using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private int enemyMeleeDamage = 1;
    private Enemy enemy;
    public HealthBarScript healthBar;
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
                Animator animator = other.gameObject.GetComponent<Animator>();
                if (player.canTakeDamage)
                {
                    
                    animator.SetTrigger("GotHit");
                    player.TookDamage();
                    player.lifePoints -= enemyMeleeDamage;
                    healthBar.SetHealth(player.lifePoints);
                }
            }
        }
    }
}
