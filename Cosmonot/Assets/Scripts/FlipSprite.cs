using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public Transform player; // drag the player object into this field in the Inspector

    void Update()
    {
        // flip the sprite based on the player's position
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
