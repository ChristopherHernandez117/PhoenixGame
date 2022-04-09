using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private GameObject model;
    private Sword sword;
    private Animator animator;
    public Vector3 movement;
    public int lifePoints = 10; // The life points of the player
    private float speed = 10.0f; // The speed of the player
    private float speedWhenDefending = 3.0f;
    public bool canTakeDamage = true;
    public bool isDefending = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        sword = GetComponentInChildren<Sword>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerAttackInput();
    }
    void OnTriggerEnter(Collider other)
    {
       
    }
    private void PlayerAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // if not available to use (still cooling down) just exit
            if (!sword.isAvailable) return;
            sword.useSword();
            animator.SetTrigger("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("IsDefending", true);
            isDefending = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("IsDefending", false);
            isDefending = false;
        }
    }
    private void PlayerMovement()
    {
        // Determine XZ movement
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Ensure diagonal movement doesn't exceed horiz/vert movement speed
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        if (movement.magnitude > 0)
        {
            RotateModelToFaceMovement(movement);
        }

        // Determine XZ movement speed
        if (isDefending)
        {
            movement *= speedWhenDefending;
        }
        else
        {
            movement *= speed;
        }
        // Move the player (via the CharacterController)
        controller.Move(movement * Time.deltaTime);
    }
    void RotateModelToFaceMovement(Vector3 moveDirection)
    {
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * 25);
    }
    public void TookDamage()
    {
        // if not available to use (still cooling down) just exit
        if (!canTakeDamage) return;
        canTakeDamage = false;
        StartCoroutine(tookDamageCountdown(4));
    }
    private IEnumerator tookDamageCountdown(int cooldownDuration)
    {
        int counter = cooldownDuration;
        while (counter > 0)
        {
            yield return new WaitForSeconds(0.1f);
            counter--;
        }
        canTakeDamage = true;
        //Debug.Log("Can take damage");
    }

}
