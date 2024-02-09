using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutPaddleController : MonoBehaviour
{
    private float xLoc = 0; // middle of screen
    [SerializeField] private float paddleSpeed = .1f;
    [SerializeField] private KeyCode leftKey, rightKey;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(leftKey) && transform.position.x > -7f)
        {
            xLoc -= paddleSpeed;
        }
        if (Input.GetKey(rightKey) && transform.position.x < 7f)
        {
            xLoc += paddleSpeed;
        }
        this.transform.position = new Vector2(xLoc, transform.position.y);
    }
}
