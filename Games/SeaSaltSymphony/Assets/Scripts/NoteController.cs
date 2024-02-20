using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int scoreBonus = 10;

    public bool ignoreEnemies = true;

    void Awake()
    {
        // using transform.Translate is unreliable, instead, we get the rigidbody component...
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        ignoreEnemies = true;
    }

    void FixedUpdate()
    {
        Vector2 delta = Vector2.left * (speed * Time.fixedDeltaTime);
        //...and we move its position instead! c:
        rb.MovePosition((Vector2)transform.position + delta);
    }

    public void FlipSpeed()
    {
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case Const.ENEMY_TAG:
                if (!ignoreEnemies)
                {
                    other.gameObject.GetComponent<EnemyController>()?.Damage();
                    Destroy(this.gameObject);
                }
                break;
            case Const.PLAYER_TAG:
                
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    if (player.IsBlocking)
                    {
                        ignoreEnemies = false;
                        if (GameManager.Instance.lives < 6)
                        {
                            GameManager.Instance.UpdateLives(1);
                        }
                        GameManager.Instance.UpdateScore(scoreBonus);
                        FlipSpeed();
                    }
                    else
                    {
                        GameManager.Instance.UpdateLives(-1);
                        Destroy(this.gameObject);
                    }
                }
                
                Debug.Log("Speed changed");
                break;
        }
    }
}