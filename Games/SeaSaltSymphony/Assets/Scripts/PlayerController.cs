using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float yLoc = 0; // middle of screen
    [SerializeField] private float laneWidth = 5f;
    [SerializeField] private KeyCode upKey, downKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(downKey) && transform.position.y > -2f)
        {
            yLoc -= laneWidth;
        }
        if (Input.GetKey(upKey) && transform.position.y < 2f)
        {
            yLoc += laneWidth;
        }
        this.transform.position = new Vector2(transform.position.x, yLoc);
    }
}
