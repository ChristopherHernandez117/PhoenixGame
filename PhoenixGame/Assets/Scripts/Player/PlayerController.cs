using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private GameObject model;
    private Sword sword;
    private Shield shield;
    private Animator animator;
    public Vector3 movement;
    private int maxLifePoints = 10;
    public int lifePoints; // The life points of the player
    public float speed; // The speed of the player
    //private float gravity = -9.81f;
    //private float yVelocity = 0f;
    private float speedWhenDefending = 3.0f;
    public bool canTakeDamage = true;
    public bool isDefending = false;
    public HealthBarScript healthBar;
    public DogHeadHUD dogHead;
    public Candelabrum candelabrum;
    public CandelabrumHUD candelabrumHUD;
    private bool isAlive = true;
   float rotateSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        lifePoints = maxLifePoints;
        healthBar.SetMaxHealth(lifePoints);
        speed = 6f;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        sword = GetComponentInChildren<Sword>();
        shield = GetComponentInChildren<Shield>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            PlayerMovement();
            PlayerAttackInput();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Candelabrum" && Input.GetKey(KeyCode.E))
        {
            Destroy(other.gameObject);
            candelabrum.gameObject.SetActive(true);
            candelabrumHUD.gameObject.SetActive(true);
        }
        if (other.gameObject.tag == "ChestWithKey" && Input.GetKey(KeyCode.E))
        {
            ChestWithKey chest = other.gameObject.GetComponent<ChestWithKey>();
            chest.DestroyMyself();
        }
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
            shield.inUse = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("IsDefending", false);
            isDefending = false;
            shield.inUse = false;
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
        
        // add gravity
        /*
        yVelocity += gravity * Time.deltaTime;

        if (controller.isGrounded && yVelocity <= 0.0)
        {
            yVelocity = 0.0f;
        } 

        movement.y = yVelocity;
        */
        
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
        Debug.Log("rmtfm");
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }
    public void TookDamage()
    {
        // if not available to use (still cooling down) just exit
        if (!canTakeDamage) return;
        if(lifePoints <= 0)
        {
            animator.SetBool("IsDead", true);
            dogHead.GetComponent<Animator>().SetBool("IsDead", true);
            isAlive = false;
        }
        dogHead.GetComponent<Animator>().SetTrigger("GotHit");
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
