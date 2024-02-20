using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb;
    public Sprite idleSprite;
    public Sprite blockSprite;
    private SpriteRenderer currentSprite;
    
    public int health = 3;
    
    private float yLoc = 0; // middle of screen
    private int currentLane = 0;
    
    [SerializeField] private KeyCode upKey = KeyCode.W, downKey = KeyCode.S, blockKey = KeyCode.L;

    public float blockTime = 1;
    public float blockCooldownTime = 1;
    
    private float blockTimer;
    private float blockCooldownTimer;

    
    public bool IsBlocking { get => blockTimer > 0; }
    public bool IsBlockingOnCooldown { get => blockCooldownTimer > 0; }
    
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (currentSprite == null) currentSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentLane = gameManager.startLane;
        transform.position = new Vector2(gameManager.playerAxisX, gameManager.GetLaneYPos(currentLane));
    }

    void Update()
    {
        if (Input.GetKeyDown(downKey) && currentLane > 1)
        {
            MovePlayer(-1);
        }
        else if (Input.GetKeyDown(upKey) && currentLane < gameManager.nbLanes)
        {
            MovePlayer(1);
        }

        if (!IsBlockingOnCooldown && !IsBlocking && Input.GetKeyDown(blockKey))
        {
            Block();
        }

        if (IsBlocking)
        {
            blockTimer -= Time.deltaTime;
            if (blockTimer <= 0) {
                StopBlock();
            }
        }
        else if (IsBlockingOnCooldown)
        {
            blockCooldownTimer -= Time.deltaTime;
            if (blockCooldownTimer <= 0) {
                StopBlockCooldown();
                blockCooldownTimer = 0;
            }
        }
    }

    public void MovePlayer(int nbOfLanes)
    {
        currentLane += nbOfLanes;
        transform.position = new Vector2(gameManager.playerAxisX, gameManager.GetLaneYPos(currentLane));
    }

    private void Block()
    {
        if (IsBlocking || IsBlockingOnCooldown) return;
        blockTimer = blockTime;
        currentSprite.sprite = blockSprite;
    }

    private void StopBlock()
    {
        blockCooldownTimer = blockCooldownTime;
        blockTimer = 0;
        currentSprite.sprite = idleSprite;
    }
    
    private void StopBlockCooldown()
    {
        blockTimer = 0;
        blockCooldownTimer = 0;
    }
    
    public void Damage()
    {
        health--;
    }
}
