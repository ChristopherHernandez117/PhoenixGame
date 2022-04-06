using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private int enemyMeleeDamage = 1;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.canTakeDamage)
            {
                player.tookDamage();
                player.lifePoints -= enemyMeleeDamage;
                Debug.Log("Player Life points:" + player.lifePoints);
            }
        }
    }
    /*
    private void useAttack()
    {
        // if not available to use (still cooling down) just exit
        if (!isAvailable) return;
        // Code goes here
        inUse = true;
        isAvailable = false;
        //Debug.Log("In use");
        // start the cooldown timer
        StartCoroutine(SwordCountdown(6));
    }*/
}
