using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int lifePoints = 5;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            Debug.Log("Hit with sword");
        }
    }
}
