using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakoutBallController : MonoBehaviour

{
    public event Action onKillAreaEntered;
    public event Action onBrickHit;

    private Rigidbody2D rb;
    public float ballSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 4f;

    private int[] dirOptions = { -1, 1 };
    private int hDir, vDir;

    public AudioSource hitSFX;
    public AudioSource scoreSFX;
    public AudioSource leftPaddleSFX;
    public AudioSource rightPaddleSFX;

    public const string WALL_TAG = "Wall";
    public const string PADDLE_LEFT_TAG = "PaddleLeft";
    public const string RESET_AREA_TAG = "ResetArea";
    public const string BRICK_TAG = "Brick";

    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Reset();
    }

    void Update()
    {
        SpeedCheck();
        if (Input.GetKeyDown(KeyCode.Space) && !isRunning)
        {
            StartCoroutine("Launch");
        }
    }

    public IEnumerator Launch()
    {
        isRunning = true;
        // choose horizontal and vertical direction options
        hDir = dirOptions[UnityEngine.Random.Range(0, dirOptions.Length)]; // randomized based on dirOptions
        vDir = -1;

        // add horizontal force
        rb.AddForce(transform.right * ballSpeed * hDir); // left or right
        // add vertical force
        rb.AddForce(transform.up * ballSpeed * vDir); // up or down

        yield return null;
    }

    private void Reset()
    {
        isRunning = false;
        rb.velocity = Vector2.zero;
        ballSpeed = 2;
        transform.position = new Vector2(0, 0);
    }

    // out of bounds checks
    private void OnCollisionEnter2D(Collision2D other)
    {
        // check if ball sped up or slowed down too much
        SpeedCheck();

        
        switch (other.gameObject.tag)
        {
            case WALL_TAG:
                hitSFX?.Play();
                break;

            case PADDLE_LEFT_TAG:
                leftPaddleSFX?.Play();
                break;

            case RESET_AREA_TAG:
                onKillAreaEntered?.Invoke();
                Reset();
                break;

            case BRICK_TAG:
                onBrickHit?.Invoke();
                Destroy(other.gameObject);
                break;

        }
    }

    private void SpeedCheck()
    {

        // Prevent ball from going too fast
        if (Mathf.Abs(rb.velocity.x) > maxSpeed) rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        if (Mathf.Abs(rb.velocity.y) > maxSpeed) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);

        if (Mathf.Abs(rb.velocity.x) < minSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x * 2f, rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) < minSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 2f);
        }
        // Debug.Log(rb.velocity);
    }
}
