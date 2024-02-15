using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakoutScoreboard : MonoBehaviour
{
    public BreakoutBallController breakoutBallController;
    public TextMeshProUGUI _scoreLabel;
    public static BreakoutScoreboard Instance;
    private int score = 0;

    private void Awake()
    {

        if (breakoutBallController != null)
        {
            // += means we subscribe to the event, and add a function to occur when it happens! c:
            BreakoutBrick.onBrickHit += OnBrickHit;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBrickHit(int value)
    {
        score += value;
        _scoreLabel.text = score.ToString();
    }
}
