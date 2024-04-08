using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineController : MonoBehaviour
{
    public SpriteRenderer renderer;
    public List<BoxCollider2D> colliders;
    [Space] public Vector2 size = Vector2.one;
    
    private void OnValidate()
    {
        if (renderer != null) renderer.size = size;
        foreach (BoxCollider2D col in colliders)
        {
            if (col == null) continue;
            col.size = size;
            col.offset = new Vector2(0, -size.y / 2);
        }
    }
}