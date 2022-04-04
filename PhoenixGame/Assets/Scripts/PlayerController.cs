using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    [SerializeField] private GameObject model;

    private float speed = 10.0f; // The speed of the player

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determine XZ movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Ensure diagonal movement doesn't exceed horiz/vert movement speed
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        if (movement.magnitude > 0)
        {
            RotateModelToFaceMovement(movement);
        }
        // Sets "IsRunning" to true if we the player is moving, otherwise sets to false
        if (movement.magnitude > 0.1)
        {
            anim.SetBool("IsRunning", true);
        } else
        {
            anim.SetBool("IsRunning", false);
        }
        // Determine XZ movement speed
        movement *= speed;
        // Move the player (via the CharacterController)
        controller.Move(movement * Time.deltaTime);
    }
    void RotateModelToFaceMovement(Vector3 moveDirection)
    {
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * 25);
    }
}
