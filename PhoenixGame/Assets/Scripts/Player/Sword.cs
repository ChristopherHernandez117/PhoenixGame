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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            useSword();
        }
    }
    private void useSword()
    {
        // if not available to use (still cooling down) just exit
        if (!isAvailable) return;
        // Code goes here
        inUse = true;
        isAvailable = false;
        //Debug.Log("In use");
        // start the cooldown timer
        StartCoroutine(SwordCountdown(6));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(inUse)
            {
                Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
                if (enemy.canTakeDamage)
                {
                    enemy.tookDamage();
                    enemy.gotHit();
                    enemy.lifePoints -= swordDamage;
                    Debug.Log("Hit with sword: " + enemy.lifePoints);
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
        //Debug.Log("Not in use");
    }

}
