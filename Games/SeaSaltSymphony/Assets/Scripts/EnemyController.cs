using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    public Rigidbody2D rb;
    public Animator anim;
    [HideInInspector]public float speed;
    
    public int health;
    public int nbProjectiles = 1;
    public float projectileDelayRatio = 1f;
    [HideInInspector]public float projectileDelay;
    

    private int projectileIndex;
    private float projectileTimer;
    
    public NoteController note;
    public GameObject hitFX;
    public GameObject deathFX;

    public string fadeAnimParam = "fade";

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        projectileTimer = 0;
        projectileIndex = 1;
    }

    void Update()
    {
        if (projectileIndex <= nbProjectiles)
        {
            projectileTimer += Time.deltaTime;
            if (projectileTimer >= projectileIndex * projectileDelay * projectileDelayRatio)
            {
                ThrowProjectile();
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 delta = Vector2.left * (speed * Time.fixedDeltaTime);
        rb.MovePosition((Vector2)transform.position + delta);
    }

    public void Damage()
    {
        health--;
        if (hitFX != null)
            Instantiate(hitFX, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            if (deathFX != null) 
                Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void ThrowProjectile()
    {
        NoteController newNote = Instantiate(note.gameObject, transform.position, Quaternion.identity).GetComponent<NoteController>();
        newNote.speed = (gameManager.shootAxisX - GameManager.Instance.noteAxisX) 
            / GameManager.Instance.songData.beatLength;
        projectileIndex++;
    }
}
