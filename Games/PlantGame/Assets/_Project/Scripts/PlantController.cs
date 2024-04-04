using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public SpriteRenderer renderer;
    public BoxCollider2D plantCol;
    public BoxCollider2D interactibleCol;
    public Animator anim;

    [Space] public Vector2 size = Vector2.one;
    public bool isAlive;
    public int magicCost = 10;
    
    [Header("Animation")] 
    public string isAliveAnimTag = "isAlive";
    
    private void Start()
    {
        if (anim == null) anim = GetComponentInChildren<Animator>();
        
        if (isAlive)
        {
            Revive();
        }
        else
        {
            Kill();
        }
    }
    
    public void Kill()
    {
        isAlive = false;
        anim?.SetBool(isAliveAnimTag, false);
        // disable collider - how?
        interactibleCol.enabled = false;
        GameManager.Instance.UpdateMagic(magicCost);
    }

    public void Revive()
    {
        isAlive = true;
        anim?.SetBool(isAliveAnimTag, true);
        // enable collider - how?
        interactibleCol.enabled = true;
        GameManager.Instance.UpdateMagic(-magicCost);
    }

    private void Reset()
    {
        interactibleCol = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnValidate()
    {
        magicCost = Math.Max(magicCost, 0);

        if (renderer != null) renderer.size = size;
        if (plantCol != null)
        {
            plantCol.size = size;
            plantCol.offset = new Vector2(0, -size.y / 2);
        }

        if (interactibleCol != null)
        {
            interactibleCol.size = size;
            interactibleCol.offset = new Vector2(0, -size.y / 2);
        }
    }
}
