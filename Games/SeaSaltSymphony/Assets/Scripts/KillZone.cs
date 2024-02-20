using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case Const.ENEMY_TAG:
                Destroy(other.gameObject);
                break;
            case Const.PROJECTILE_TAG:
                GameManager.Instance.UpdateLives(-1);
                Destroy(other.gameObject);
                break;
        }
    }
}
