using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int points;
    public BreakoutBallController breakoutBallController;
    public Image livesImage;

    public static GameManager S;
    // Awake happens BEFORE start
    void Awake()
    {
        if (S) return;
        S = this;
        if(breakoutBallController != null)
        {
            breakoutBallController.onKillAreaEntered += OnKillAreaEntered;
        }
    }

    void Start()
    {
        lives = 3;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (breakoutBallController != null)
        {
            breakoutBallController.onKillAreaEntered -= OnKillAreaEntered;
        }
    }

    public void OnKillAreaEntered()
    {
        lives -= 1;
        livesImage.fillAmount = lives * 0.33f;
    }

   
}
