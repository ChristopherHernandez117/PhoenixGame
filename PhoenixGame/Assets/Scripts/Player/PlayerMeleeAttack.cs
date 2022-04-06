using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Sword sword;
    private Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
        player = animator.gameObject.GetComponentInParent<PlayerController>();
        //animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // if not available to use (still cooling down) just exit
            if (!sword.isAvailable) return;
            sword.useSword();
            animator.SetTrigger("Attack");    
        } else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("IsDefending", true);
            player.isDefending = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1)) {
            animator.SetBool("IsDefending", false);
            player.isDefending = false;
        }
    }
}
