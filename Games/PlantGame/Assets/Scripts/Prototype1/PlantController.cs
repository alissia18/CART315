using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public Collider2D interactibleCol;
    public Animator anim;
    
    [Space]
    
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
        interactibleCol = GetComponentInChildren<Collider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnValidate()
    {
        magicCost = Math.Max(magicCost, 0);
    }
}
