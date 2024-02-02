using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private float yLoc = 0; // middle of screen
    [SerializeField] private float paddleSpeed = .1f;
    [SerializeField] private KeyCode upKey, downKey;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(downKey) && yLoc > -5f)
        {
            yLoc -= paddleSpeed;
        }
        if (Input.GetKey(upKey) && yLoc < 5f)
        {
            yLoc += paddleSpeed;
        }
        this.transform.position = new Vector2(transform.position.x, yLoc);
    }
}
