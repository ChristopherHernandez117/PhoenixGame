using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
        playerController = animator.gameObject.GetComponentInParent<PlayerController>();
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
    }
}
