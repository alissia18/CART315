using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    public BallController ballController;
    public Player _player;

    public TextMeshProUGUI _scoreLabel;

    private void Awake()
    {
        if (ballController != null)
        {
            // += means we subscribe to the event, and add a function to occur when it happens! c:
            ballController.onPlayerScoreUpdate += OnPlayerScoreUpdate;
           
        }
    }

    private void Start()
    {
        if (_player == Player.LEFT) 
            OnPlayerScoreUpdate(Player.LEFT, ballController.leftPlayerScore);
        else if (_player == Player.RIGHT) 
            OnPlayerScoreUpdate(Player.RIGHT, ballController.rightPlayerScore);
    }

    private void OnDestroy()
    {
        if (ballController != null)
        {
            ballController.onPlayerScoreUpdate -= OnPlayerScoreUpdate;
        }
    }

    public void OnPlayerScoreUpdate(Player player, int score)
    {
        if (player == _player)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}
