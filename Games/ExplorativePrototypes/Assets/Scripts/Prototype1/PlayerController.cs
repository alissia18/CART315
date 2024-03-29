using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Tags")] 
    public const string CLIMBABLE_TAG = "Climbable";
    
    [Header("Movement")]
    
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpVelocity = 6.5f;
    public int maxJumps = 1;
    public bool facingRight = true;
    
    private int currentJumps;
    
    private bool isGrounded = false;
    
    [Header("Ground check")]
    
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;
    
    [Header("Abilities")]
    public Transform shootingPos;
    public float projectileSpeed = 5f;
    public ProjectileController projectileController;
    public LayerMask plantsLayer;
    public float plantsVacuumRadius = 3.5f;
    
    private bool isClimbing;
    private Collider2D currentInteractibleCol;
    public LayerMask interactibleLayer;
    
    private Rigidbody2D currentProjectile;
    
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string JUMP_INPUT = "Jump";
    private const string VERTICAL_AXIS = "Vertical";

    private Vector2 move_input;
    
    private Camera camera;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        move_input.x = Input.GetAxis(HORIZONTAL_AXIS); //gives a value from -1 to 1, -1 being a and 1 being d. Smoothing is applied (awwegedwy :0)
        move_input.y = Input.GetAxis(VERTICAL_AXIS);

        // jump with space
        if (Input.GetButtonDown(JUMP_INPUT)) Jump();

        // shoot with left click
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.magicLeft > 0)
            {
                Shoot();
            }
            
        }
        // vaccuum while right click is pressed
        if (Input.GetMouseButton(1))
        {
            Vacuum();
        }
        // start climbing 
        if (!isClimbing && currentInteractibleCol && move_input.y != 0 && (move_input.y > 0 || !isGrounded))
        {
            StartClimbing();
        }
    }

    private void FixedUpdate()
    {
        Vector2 v = rb.velocity;
        if (isClimbing)
        {
            v.y = move_input.y * maxSpeed;
        }
        else
        {
            v.x = move_input.x * maxSpeed;
        }
        rb.velocity = v;

        Collider2D ground = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (ground && !isGrounded)
        {
            isGrounded = true;
            currentJumps = 0;
            StopClimbing();
        }
        else if (!ground && isGrounded)
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
        if (isClimbing)
        {
            if (move_input.y >= 0)
            {
                rb.velocity = new Vector2(move_input.x * maxSpeed, jumpVelocity);
            }
            
            StopClimbing();
        }
        else if (currentJumps < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            currentJumps++;
        }
    }
    
    public void Shoot()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = (mousePosition - shootingPos.position).normalized * projectileSpeed;

        currentProjectile = Instantiate(projectileController.gameObject, shootingPos).GetComponent<Rigidbody2D>();
        currentProjectile.velocity = velocity;
    }

    public void Vacuum()
    {
        Collider2D vacuumPlant = Physics2D.OverlapCircle(shootingPos.position, plantsVacuumRadius, plantsLayer);
        
        if (vacuumPlant)
        {
            PlantController plant = vacuumPlant.GetComponent<PlantController>();
            if (plant && plant.isAlive)
            {
                plant.Kill();
            }
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;
        rb.velocity = Vector2.zero;
        Vector3 pos = transform.position;
        pos.x = currentInteractibleCol.transform.position.x;
        transform.position = pos;

        currentJumps = 0;
        
        rb.gravityScale = 0f;
        Debug.Log("I'm climbing now");
    }

    private void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = 1f;
        Debug.Log("I'm not climbing anymore :c");
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        switch (otherCollider.gameObject.tag)
        {
            case CLIMBABLE_TAG:
                currentInteractibleCol = otherCollider;
                Debug.Log("Found a plant to climb");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider == currentInteractibleCol)
        {
            if (isClimbing)
            {
                StopClimbing();
            }
            Debug.Log("Leaving the plant");

            currentInteractibleCol = null;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(shootingPos.position, plantsVacuumRadius);
    }
}
