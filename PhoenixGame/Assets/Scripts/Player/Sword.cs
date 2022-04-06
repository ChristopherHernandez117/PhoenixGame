using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool isAvailable;
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
        // start the cooldown timer
        StartCoroutine(SwordCountdown(6));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inUse)
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            if (enemy.alive)
            {
                if (enemy.canTakeDamage)
                {
                    enemy.tookDamage();
                    enemy.gotHit();
                    enemy.lifePoints -= swordDamage;
                    Debug.Log("Hit with sword: " + enemy.lifePoints);

                    enemy.gameObject.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, 0, 1000000));
                }
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
    }

}
