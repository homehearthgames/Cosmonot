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
        if (playerMovement.moveInput.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (playerMovement.moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
