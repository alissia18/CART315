using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutBrick : MonoBehaviour
{
    public int scoreValue;
    // static so that we don't need any specific brick to call it c:
    public static event Action<int> onBrickHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnHit()
    {
        onBrickHit?.Invoke(scoreValue);
        Destroy(gameObject);
    }
}
