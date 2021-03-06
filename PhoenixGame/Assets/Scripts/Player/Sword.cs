using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private PlayerController player; 
    public bool isAvailable = true;
    private bool inUse;
    private int swordDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        isAvailable = true;
        inUse = false;
    }

    public void useSword()
    {
        // Code goes here
        inUse = true;
        isAvailable = false;
        player.speed = 4;
        // start the cooldown timer
        StartCoroutine(SwordCountdown(6));
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy2" && inUse)
        {
            other.GetComponent<Enemy2>().ReactToHit();
        }

        if (other.gameObject.tag == "Enemy" && inUse)
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            if (enemy.alive && enemy.canTakeDamage)
            {
                enemy.HitWithSword();
                enemy.lifePoints -= swordDamage;
            }
        }
    }
    private IEnumerator SwordCountdown(int cooldownDuration)
    {
        int counter = cooldownDuration;
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.1f);
            counter--;
        }
        inUse = false;
        isAvailable = true;
        player.speed = 6f;
    }

}
