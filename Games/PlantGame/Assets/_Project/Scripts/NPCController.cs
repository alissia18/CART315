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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        switch (otherCol.gameObject.tag)
        {
            case PLAYER_TAG:
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
