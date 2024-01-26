using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    private SpriteRenderer sr;
    public float xLoc = 0; // middle of screen
    public float bedSpeed = .1f;

    public float score = 0; // normally should not be in this script

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && xLoc > -7f)
        {
            xLoc -= bedSpeed;
            transform.eulerAngles = new Vector3(0, 0, -10f);
        }
        if (Input.GetKey(KeyCode.X) && xLoc < 7f)
        {
            xLoc += bedSpeed;
            transform.eulerAngles = new Vector3(0, 0, 10f);
        }
        this.transform.position = new Vector2(xLoc, transform.position.y);
     
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Sleepy") score++;
        else score--;

        Destroy(other.gameObject); // RIP QwQ
    }
}
