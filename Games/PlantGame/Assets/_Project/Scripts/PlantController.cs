using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public BoxCollider2D interactibleCol;
    public Animator anim;

    public bool isAlive;
    public int magicCost = 1;
    
    [Header("Animation")] 
    public string isAliveAnimTag = "isAlive";
    
    protected void Start()
    {
        if (anim == null) anim = GetComponentInChildren<Animator>();
        SetAlive(isAlive);
    }
    
    public virtual void Kill()
    {
        SetAlive(false);
        GameManager.Instance.UpdateMagic(magicCost);
    }

    public virtual void Revive()
    {
        SetAlive(true);
        GameManager.Instance.UpdateMagic(-magicCost);
    }

    public virtual void SetAlive(bool value)
    {
        isAlive = value;
        interactibleCol.enabled = isAlive;
        anim?.SetBool(isAliveAnimTag, isAlive);
    }

    private void OnValidate()
    {
        magicCost = Math.Max(magicCost, 0);
    }
    
    private void Reset()
    {
        interactibleCol = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }
}

