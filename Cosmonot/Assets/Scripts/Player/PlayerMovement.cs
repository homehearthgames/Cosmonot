using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    Rigidbody2D playerRb;
    public Vector2 moveInput;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        playerRb.MovePosition(playerRb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
