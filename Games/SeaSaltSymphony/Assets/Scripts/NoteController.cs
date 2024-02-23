using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int scoreBonus = 10;

    public List<AccuracyBracket> accuracyBrackets = new List<AccuracyBracket>();

    private bool flipped;

    void Awake()
    {
        // using transform.Translate is unreliable, instead, we get the rigidbody component...
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        flipped = false;
    }

    void FixedUpdate()
    {
        Vector2 delta = Vector2.left * (speed * Time.fixedDeltaTime);
        //...and we move its position instead! c:
        rb.MovePosition((Vector2)transform.position + delta);
    }

    public void FlipSpeed()
    {
        flipped = true;
        speed *= -1;
        float accuracyScore = transform.position.x - GameManager.Instance.noteAxisX;

        for (int i = accuracyBrackets.Count - 1; i >= 0; i-- )
        {
            if (accuracyScore >= accuracyBrackets[i].lowerBound || i == 0)
            {
                if (accuracyBrackets[i] != null)
                    Instantiate(accuracyBrackets[i].fx, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case Const.ENEMY_TAG:
                if (flipped)
                {
                    other.gameObject.GetComponent<EnemyController>()?.Damage();
                    Destroy(this.gameObject);
                }
                break;
            case Const.PLAYER_TAG:
                if (!flipped)
                {
                    PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();
                    if (player != null)
                    {
                        if (player.IsBlocking)
                        {
                            if (GameManager.Instance.lives < 6)
                            {
                                GameManager.Instance.UpdateLives(1);
                            }
                            GameManager.Instance.UpdateScore(scoreBonus);
                            FlipSpeed();
                            player.StopBlock();
                        }
                        else
                        {
                            GameManager.Instance.UpdateLives(-1);
                            Destroy(this.gameObject);
                        }
                    }

                    Debug.Log("Speed changed");
                    
                }
                break;
        }
    }

    [Serializable]
    public class AccuracyBracket
    {
        public GameObject fx;
        public float lowerBound;
        public int scoreBonus = 10;
    }
}