using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineController : PlantController
{
    public BoxCollider2D interactibleCol;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    private void Reset()
    {
        interactibleCol = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public override void Kill()
    {
        isAlive = false;
        anim?.SetBool(isAliveAnimTag, false);
        interactibleCol.enabled = false;
        GameManager.Instance.UpdateMagic(magicCost);
    }

    public override void Revive()
    {
        isAlive = true;
        anim?.SetBool(isAliveAnimTag, true);
        // enable collider - how?
        interactibleCol.enabled = true;
        GameManager.Instance.UpdateMagic(-magicCost);
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