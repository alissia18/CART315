using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onPlayerScoreUpdate += OnPlayerScoreUpdate;
        scoreText.text = GameManager.Instance.score.ToString();
    }

    void OnDestroy()
    {
        GameManager.Instance.onPlayerScoreUpdate -= OnPlayerScoreUpdate;
    }

    public void OnPlayerScoreUpdate()
    {
        scoreText.text = GameManager.Instance.score.ToString();
    }
}