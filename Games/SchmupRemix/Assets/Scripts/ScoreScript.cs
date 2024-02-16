using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
   
{
    public Text scoreText;
    public int score;
    public static ScoreScript S;
    // Start is called before the first frame update
    void Start()
    {
        S = this; // singleton
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 1;
            scoreText.text = "Score: " + score;
        }
    }
}
