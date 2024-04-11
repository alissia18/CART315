using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    private bool itemCollected;

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if (itemCollected) return;

        switch (otherCol.gameObject.tag)
        {
            case PLAYER_TAG:
                GameManager.Instance.CollectItem();
                itemCollected = true;
                Destroy(gameObject);
                break;
        }
    }
}
