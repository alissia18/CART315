using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickLayerManager : MonoBehaviour
{
    private GameObject brick;
    public GameObject[] bricks = new GameObject[3];
    public int rows, columns;
    public float brickSpacing_h, brickSpacing_v;
    private float xPos, yPos;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                brick = bricks[Random.Range(0, bricks.Length)];
                xPos = 3.5f + -columns + (i * brickSpacing_h);
                yPos = rows - (j * brickSpacing_v) -1f;
                Instantiate(brick, new Vector3(xPos, yPos, 0), Quaternion.identity);
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
