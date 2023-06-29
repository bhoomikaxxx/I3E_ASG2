using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //HealthBar
    int currentHealth = 20;
    int maxHealth = 20;
    public HPBar hpBar;

    //Count of collectibles
    int count = 0;

    //HP Bar measures
    private void OnCollisionEnter(Collision collision)
    {
        //Points added if player collects
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            currentHealth += -1;
            hpBar.UpdateHpBar(maxHealth, currentHealth);
        }
    }



    [Header("Movement")]
    //Can change speed
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    //Change friction with ground 
    public float groundDrag;

    //Change jump mechanics
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;

    //Key to for movement
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;


    //Checks for ground
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask GroundLayerM;
    bool grounded;

    //Camera orientation
    public Transform orientation;

    //Values from axis
    float horizontalnput;
    float verticalnput;

    Vector3 moveDirection;

    //Movement Data
    public MovementState state;

    //Define states
    public enum MovementState
    {
        walking,
        sprinting,
        air,
    }

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Keep player from moving + falling
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Set active jump
        readyToJump = true;

        //For HPBar
        hpBar = FindObjectOfType<HPBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, GroundLayerM);

        //Constantly checks player transform
        MyInput();

        //Handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //State
        StateHandler();
    }

    private void FixedUpdate()
    {
        //Move player
        MovePlayer();
    }

    private void MyInput()
    {
        //Get data input from axis
        horizontalnput = Input.GetAxisRaw("Horizontal");
        verticalnput = Input.GetAxisRaw("Vertical");

        //When need to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }

    }

    private void StateHandler()
    {
        //Sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            currentHealth += -1;

        }

        //Walking
        else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        //In air
        else
        {
            state = MovementState.air;

        }
    }

    private void MovePlayer()
    {
        //Calculate move direction
        moveDirection = orientation.forward * verticalnput + orientation.right * horizontalnput;

        //On ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //In air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        }
    }

    private void Jump()
    {
        //To reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}

