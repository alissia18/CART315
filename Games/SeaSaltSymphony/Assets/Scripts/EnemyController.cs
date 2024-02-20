using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    [HideInInspector]public float speed;
    
    public int health;
    public int nbProjectiles = 1;
    [HideInInspector]public float projectileDelay;
    

    private int projectileIndex;
    private float projectileTimer;
    
    public GameObject note;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        projectileTimer = 0;
        projectileIndex = 1;
    }

    void Update()
    {
        if (projectileIndex <= nbProjectiles)
        {
            projectileTimer += Time.deltaTime;
            if (projectileTimer >= projectileIndex * projectileDelay)
            {
                GameObject newNote = Instantiate(note, transform.position, Quaternion.identity);
                projectileIndex++;
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
        if (health <= 0)
            Destroy(gameObject);
    }

    
}
