using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private PlayerInput playerInput;
    private Rigidbody2D[] parts;


    [SerializeField] private Vector2 jumpForce = new Vector2(10f, 10f);

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        parts = GetComponentsInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(playerInput.actions["Direction"].ReadValue<Vector2>());
        direction.z = 0f;   ///no changing depth

        if (playerInput.actions["Jump"].triggered)      //Add groundcheck
        {
            direction = Vector3.Normalize(direction - transform.position);
            foreach (Rigidbody2D part in parts)
            {
                part.AddForce(jumpForce * direction, ForceMode2D.Impulse);
            }
        }
    }

    //private bool IsGrounded()
    //{
    //    return Physics2D.OverlapCircle()    //check for each child collider?
    //}
}
