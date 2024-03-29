using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { LEFT, RIGHT };

public class BallController : MonoBehaviour

{
    public event Action<Player, int> onPlayerScoreUpdate;

    private Rigidbody2D rb;
    public float ballSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 4f;

    private int[] dirOptions = { -1, 1 };
    private int hDir, vDir;

    public int leftPlayerScore, rightPlayerScore;

    public AudioSource hitSFX;
    public AudioSource scoreSFX;
    public AudioSource leftPaddleSFX;
    public AudioSource rightPaddleSFX;

    public const string WALL_LEFT_TAG = "WallLeft";
    public const string WALL_RIGHT_TAG = "WallRight";
    public const string WALL_TAG = "Wall";
    public const string PADDLE_LEFT_TAG = "PaddleLeft";
    public const string PADDLE_RIGHT_TAG = "PaddleRight";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Reset();
    }

    void Update()
    {
        SpeedCheck();
    }

    public IEnumerator Launch()
    {
        yield return new WaitForSeconds(1.5f);

        // choose horizontal and vertical direction options
        hDir = dirOptions[UnityEngine.Random.Range(0, dirOptions.Length)]; // randomized based on dirOptions
        vDir = dirOptions[UnityEngine.Random.Range(0, dirOptions.Length)];

        // add horizontal force
        rb.AddForce(transform.right * ballSpeed * hDir); // left or right
        // add vertical force
        rb.AddForce(transform.up * ballSpeed * vDir); // up or down

        yield return null;
    }

    private void Reset()
    {
        rb.velocity = Vector2.zero;
        ballSpeed = 2;
        transform.position = new Vector2(0, -2);
        StartCoroutine("Launch");
    }

    // out of bounds checks
    private void OnCollisionEnter2D(Collision2D other)
    {
        // check if ball sped up or slowed down too much
        SpeedCheck();

        
        switch (other.gameObject.tag)
        {
            // did we hit the left wall?
            case WALL_LEFT_TAG:
                rightPlayerScore += 1;
                // if anyone is listening (?), onPlayerScoreUpdate just HAPPENED!!!
                onPlayerScoreUpdate?.Invoke(Player.RIGHT, rightPlayerScore);
                Reset();
                scoreSFX?.Play();
                break;
            // did we hit the right Wall?
            case WALL_RIGHT_TAG:
                leftPlayerScore += 1;
                onPlayerScoreUpdate?.Invoke(Player.LEFT, leftPlayerScore);
                Reset();
                scoreSFX?.Play();
                break;
            case WALL_TAG:
                hitSFX?.Play();
                break;
            case PADDLE_LEFT_TAG:
                leftPaddleSFX?.Play();
                break;
            case PADDLE_RIGHT_TAG:
                rightPaddleSFX?.Play();
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
            Debug.Log("x: " + Mathf.Abs( rb.velocity.x) + " : " + minSpeed);
            rb.velocity = new Vector2(rb.velocity.x * 2f, rb.velocity.y);
            Debug.Log("after: " + Mathf.Abs(rb.velocity.x) + " : " + minSpeed);
        }
        if (Mathf.Abs(rb.velocity.y) < minSpeed)
        {
            Debug.Log("y: " + Mathf.Abs(rb.velocity.y) + " : " + minSpeed);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 2f);
            Debug.Log("after: " + Mathf.Abs(rb.velocity.x) + " : " + minSpeed);
        }
        // Debug.Log(rb.velocity);
    }
}
