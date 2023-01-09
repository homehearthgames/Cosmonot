using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;
    
    Animator playerAnimator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayAnimations();
        FlipSprite();
    }

    private void PlayAnimations()
    {
        if (playerMovement.moveInput == new Vector2(0, 0))
        {
            playerAnimator.SetBool("isRunning", false);
        }
        else
        {
            playerAnimator.SetBool("isRunning", true);
        }
    }
    
    private void FlipSprite()
    {
        // Get the cursor position in world coordinates
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // If the cursor is to the left of the player
        if (cursorPos.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        // If the cursor is to the right of the player
        else if (cursorPos.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

}
