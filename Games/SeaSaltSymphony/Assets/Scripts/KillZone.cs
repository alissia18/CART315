using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case Const.ENEMY_TAG:
            case Const.PROJECTILE_TAG:
                Destroy(other.gameObject);
                break;
        }
    }
}
