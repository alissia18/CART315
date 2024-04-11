using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    [Header("Tags")] 
    public const string CLIMBABLE_TAG = "Climbable";

    public const string SHROOM_TAG = "Mushroom";
    public const string PLAYER_TAG = "Player";

    public bool canMove = true;

    [Header("Movement")] 
    public float gravityScale = 2f;
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

    private bool isVacuuming;
    private bool isClimbing;
    private Collider2D currentInteractibleCol;
    public LayerMask interactibleLayer;
    
    private Rigidbody2D currentProjectile;

    [Header("Feedback")] 
    
    public Material magic;
    private Material baseMaterial;

    public List<GameObject> charges = new List<GameObject>();
    private int activeCharges;
    
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string JUMP_INPUT = "Jump";
    private const string VERTICAL_AXIS = "Vertical";

    private Vector2 move_input;
    
    private Camera camera;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        baseMaterial = spriteRenderer.material;

        activeCharges = -1;
        UpdateCharges();
    }

    private void Update()
    {
        if (!canMove)
        {
            move_input.x = 0;
            move_input.y = 0;

            isVacuuming = false;
            if (isClimbing) StopClimbing();
            return;
        }

        //gives a value from -1 to 1, -1 being a and 1 being d. Smoothing is applied (awwegedwy :0)
        move_input.x = Input.GetAxis(HORIZONTAL_AXIS);
        move_input.y = Input.GetAxis(VERTICAL_AXIS);
        
        // vaccuum while right click is pressed
        if (!isVacuuming && Input.GetMouseButtonDown(1))
        {
            isVacuuming = true;
        }
        else if (isVacuuming && Input.GetMouseButtonUp(1))
        {
            isVacuuming = false;
        }
        
        // shoot with left click
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.magicLeft > 0)
            {
                Shoot();
            }
        }
        
        if (isVacuuming)
        {
            Vacuum();
        }
        // actions we can't do while vacuuming
        // start climbing 
        else if (!isClimbing && currentInteractibleCol && move_input.y != 0 && (move_input.y > 0 || !isGrounded))
        {
            StartClimbing();
        }
        // jump with space
        else if (Input.GetButtonDown(JUMP_INPUT)) Jump();

        HandleAnimations();
        UpdateCharges();
    }

    private void FixedUpdate()
    {
        Vector2 v = rb.velocity;

        if (!canMove)
        {
            v.x = 0;
            rb.velocity = v;
            return;
        }

        if (isVacuuming)
        {
            v = Vector2.zero;
        }
        else if (isClimbing)
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
        isVacuuming = false;
        anim.SetTrigger("TwirlAttack");
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 velocity = (mousePosition - shootingPos.position).normalized * projectileSpeed;

        currentProjectile = Instantiate(projectileController.gameObject, shootingPos).GetComponent<Rigidbody2D>();
        currentProjectile.velocity = velocity;
    }

    public void Vacuum()
    {
        List<Collider2D> plants = Physics2D.OverlapCircleAll(shootingPos.position, plantsVacuumRadius, plantsLayer).ToList();
        plants = plants.OrderBy(p => (p.transform.position - transform.position).sqrMagnitude).ToList();

        foreach (Collider2D plant in plants)
        {
            PlantController controller = plant.GetComponent<PlantController>();
            if (controller && controller.isAlive) {
                controller.Kill();
                break;
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
    }

    private void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = gravityScale;
    }

    public void HandleAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsJumping", !isGrounded);
        anim.SetBool("IsClimbing", isClimbing);
        anim.SetBool("IsVacuuming", isVacuuming);
        
        //change character direction
        Vector3 scale = spriteRenderer.transform.localScale;
        scale.x = (move_input.x > 0f)? Mathf.Abs(scale.x) : 
            (move_input.x < 0f)? scale.x = -Mathf.Abs(scale.x) : scale.x;
        spriteRenderer.transform.localScale = scale;
    }

    public void ActivateMagic()
    {
        spriteRenderer.material = magic;
    }

    public void DeactivateMagic()
    {
        spriteRenderer.material = baseMaterial;
    }

    public void UpdateCharges()
    {
        if (activeCharges == GameManager.Instance.magicLeft)
            return;
        
        for (int i = 0; i < charges.Count; i++)
        {
            charges[i].SetActive(i < GameManager.Instance.magicLeft);
        }

        activeCharges = GameManager.Instance.magicLeft;
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        switch (otherCollider.gameObject.tag)
        {
            case CLIMBABLE_TAG:
                currentInteractibleCol = otherCollider;
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
