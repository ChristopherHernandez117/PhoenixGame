using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private GameObject model;
    public Vector3 movement;
    public int lifePoints = 10; // The life points of the player
    private float speed = 10.0f; // The speed of the player
    public bool canTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

    }
    void OnTriggerEnter(Collider other)
    {
       
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
        movement *= speed;
        // Move the player (via the CharacterController)
        controller.Move(movement * Time.deltaTime);
    }
    void RotateModelToFaceMovement(Vector3 moveDirection)
    {
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * 25);
    }

    public void tookDamage()
    {
        // if not available to use (still cooling down) just exit
        if (!canTakeDamage) return;
        canTakeDamage = false;
        //Debug.Log("CANNOT take damage");
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
