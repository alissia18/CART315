using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    [Header("Tags")] 
    public const string CLIMBABLE_TAG = "Climbable";

    public const string SHROOM_TAG = "Mushroom";
    
    [Header("Movement")]
    
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpVelocity = 6.5f;
    public int maxJumps = 1;
    public bool facingRight = true;
    public int bounceVelocity = 6;
    
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
        //gives a value from -1 to 1, -1 being a and 1 being d. Smoothing is applied (awwegedwy :0)
        move_input.x = Input.GetAxis(HORIZONTAL_AXIS);
        if (move_input.x < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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
        else
        {
            anim.SetBool("IsVacuuming", false);
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
        anim.SetFloat("Speed", Mathf.Abs(v.x));
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
            anim.SetBool("IsJumping", false);
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
                anim.SetBool("IsJumping",true);
                rb.velocity = new Vector2(move_input.x * maxSpeed, jumpVelocity);
            }
            
            StopClimbing();
        }
        else if (currentJumps < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            currentJumps++;
            anim.SetBool("IsJumping",true);
        }
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceVelocity);
    }
    
    public void Shoot()
    {
        anim.SetTrigger("TwirlAttack");
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = (mousePosition - shootingPos.position).normalized * projectileSpeed;

        currentProjectile = Instantiate(projectileController.gameObject, shootingPos).GetComponent<Rigidbody2D>();
        currentProjectile.velocity = velocity;
    }

    public void Vacuum()
    {
        anim.SetBool("IsVacuuming", true);
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
        anim.SetBool("IsClimbing", true);
        isClimbing = true;
        rb.velocity = Vector2.zero;
        Vector3 pos = transform.position;
        pos.x = currentInteractibleCol.transform.position.x;
        transform.position = pos;

        currentJumps = 0;
        
        rb.gravityScale = 0f;
    }

    private void StopClimbing()
    {
        anim.SetBool("IsClimbing", false);
        isClimbing = false;
        rb.gravityScale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        switch (otherCollider.gameObject.tag)
        {
            case CLIMBABLE_TAG:
                currentInteractibleCol = otherCollider;
                break;
            case SHROOM_TAG:
                Bounce();
                break;
                
        }
    }
    
    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        switch (otherCollider.gameObject.tag)
        {
            case SHROOM_TAG:
                Debug.Log("I am bouncing on a shroom!");
                Bounce();
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
