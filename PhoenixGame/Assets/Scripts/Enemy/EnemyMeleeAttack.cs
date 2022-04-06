using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public bool isAvailable = true;
    private bool inUse = false;
    private int enemyMeleeDamage = 1;
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (enemy.alive)
        {
            if (other.gameObject.tag == "Player")
            {
                useAttack();
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player.canTakeDamage)
                {
                    player.tookDamage();
                    player.lifePoints -= enemyMeleeDamage;
                    Debug.Log("Player Life points:" + player.lifePoints);
                }
            }
        }
    }
    
    private void useAttack()
    {
        // if not available to use (still cooling down) just exit
        if (!isAvailable) return;
        // Code goes here
        inUse = true;
        isAvailable = false;
        // start the cooldown timer
        StartCoroutine(EnemyAttackCountDown(4));
    }
    private IEnumerator EnemyAttackCountDown(int cooldownDuration)
    {
        int counter = cooldownDuration;
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.1f);
            counter--;
        }
        inUse = false;
        isAvailable = true;
    }
}
