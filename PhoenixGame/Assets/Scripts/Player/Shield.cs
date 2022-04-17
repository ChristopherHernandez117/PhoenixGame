using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool inUse;
    // Start is called before the first frame update
    void Start()
    {
        inUse = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && inUse)
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            if (enemy.alive && enemy.canTakeDamage)
            {
                enemy.HitWithShield();
                
                Debug.Log("Hit with shield");
            }
        }
    }
}
