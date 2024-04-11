using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public string visibleTag;
    public Collider2D dialogueCol;
    public Animator textboxAnimator;

    public GameObject requestText;
    public GameObject itemCollectedText;

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        switch (otherCol.gameObject.tag)
        {
            case PLAYER_TAG:
                requestText.SetActive(!GameManager.Instance.itemCollected);
                itemCollectedText.SetActive(GameManager.Instance.itemCollected);

                if (GameManager.Instance.itemCollected)
                {
                    GameManager.Instance.CompleteLevel();
                }

                textboxAnimator.SetBool(visibleTag, true);
                break;
        }
    }
    
    void OnTriggerExit2D(Collider2D otherCol)
    {
        switch (otherCol.gameObject.tag)
        {
            case PLAYER_TAG:
                textboxAnimator.SetBool(visibleTag, false);
                break;
        }
    }
}
