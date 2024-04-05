using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public SpriteRenderer renderer;
    public BoxCollider2D plantCol;
    public Animator anim;

    [Space] public Vector2 size = Vector2.one;
    public bool isAlive;
    public int magicCost = 10;
    
    [Header("Animation")] 
    public string isAliveAnimTag = "isAlive";
    
    protected void Start()
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
    
    public virtual void Kill()
    {
        isAlive = false;
        anim?.SetBool(isAliveAnimTag, false);
        GameManager.Instance.UpdateMagic(magicCost);
    }

    public virtual void Revive()
    {
        isAlive = true;
        anim?.SetBool(isAliveAnimTag, true);
        GameManager.Instance.UpdateMagic(-magicCost);
    }
    
   
    }

