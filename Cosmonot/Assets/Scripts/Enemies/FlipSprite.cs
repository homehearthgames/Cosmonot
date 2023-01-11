using Pathfinding;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public Transform player; // drag the player object into this field in the Inspector

    AIPath a_star; 
    SpriteRenderer sprite_renderer;

    void Start() {
        a_star = GetComponent<AIPath>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // flip the sprite based on the player's position
        if (a_star.velocity.x > -0.01f)
        {
            sprite_renderer.flipX = false;
        }
        else
        {
            sprite_renderer.flipX = true;
        }
    }
}
