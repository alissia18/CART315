using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : MonoBehaviour
{
    public int bounceVelocity = 6;
    
    [Space]
    
    public Animator anim;
    public string bounceAnim = "Bounce";
    
    public void Bounce(Rigidbody2D target)
    {
        if (target == null) return;
        target.velocity = new Vector2(target.velocity.x, bounceVelocity);
        if (anim != null) anim.SetTrigger(bounceAnim);
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        switch (otherCollider.gameObject.tag)
        {
            case PlayerController.PLAYER_TAG:
                Bounce(otherCollider.GetComponent<Rigidbody2D>());
                break;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        switch (otherCollider.gameObject.tag)
        {
            case PlayerController.PLAYER_TAG:
                Bounce(otherCollider.collider.GetComponent<Rigidbody2D>());
                break;
                
        }
    }
}
