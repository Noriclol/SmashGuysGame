using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    //FIELDS
    
    //components
    public Rigidbody2D rb;
    public CircleCollider2D bot;
    public BoxCollider2D top;
    public Camera cameraRef;
    public Animator animator;
    public GameInput controls;
    private InputAction move;
    private InputAction jump;

    public SpriteRenderer MiniMapRenderer;
    //Movement
    
    public PlayerChoice chosenInput = PlayerChoice.Player1;
    private MovementForceStates moveState;
    private float movementX;
    private float movementMult = 0.5f;
    private float movementTopSpeed = 10f;
    private float LastInputX = 1.0f;

    private bool Grounded = true;
    public float jumpForce = 10f;
    public float fallMult = 50f;
    public float fallTopSpeed = 80f;
    
    //playerColor
    public Color playerColor;
    
    
    
    
    private void Awake()
    {
        controls = Main.Instance.controls;

        bot = GetComponent<CircleCollider2D>();
        top = GetComponent<BoxCollider2D>();
        cameraRef = GetComponentInChildren<Camera>();
    }
    
    
    
    
    
    

    //ENABLE/DISABLE
    
    private void OnEnable()
    {
        //SetPlayerInput();
    }

    public void SetPlayerInput(PlayerChoice player, Color C)
    {
        print((int)player);
        switch (player)
        {
            case PlayerChoice.Player1:
                move = controls.PlayerOne.Move;
                jump = controls.PlayerOne.Jump;
                break;
            
            case PlayerChoice.Player2:
                move = controls.PlayerTwo.Move;
                jump = controls.PlayerTwo.Jump;
                break;
            
            case PlayerChoice.Player3:
                break;
            case PlayerChoice.Player4:
                break;
        }

        move.Enable();


        jump.Enable();

        jump.performed += OnJump;
        //move.performed += OnMove;
        move.started += SetMovementState;
        move.canceled += SetMovementState;
        this.gameObject.GetComponent<SpriteRenderer>().color = C;
        MiniMapRenderer.color = C;
    }

    private void OnDisable()
    {
        jump.performed -= OnJump;
        move.started -= SetMovementState;
        move.canceled -= SetMovementState;;
        
        move.Disable();
        jump.Disable();
    }
    

    //EVENT FUNCTIONS

    private void FlipCharacterSprite()
    {
        var oldDirection = transform.localScale.x;
        transform.localScale = new Vector3(-oldDirection, 1, 1);
    }
    
    private void SetMovementState(InputAction.CallbackContext obj)
    {
        var input = obj.ReadValue<Vector2>();
        switch (input.x)
        {
            case -1.0f:
                moveState = MovementForceStates.Moving;
                movementX = input.x;

                if (LastInputX == input.x)
                {
                    break;
                }
                FlipCharacterSprite();
                LastInputX = input.x;
                break;
            
            case 0.0f:
                moveState = MovementForceStates.Idle;
                movementX = 0;
                break;
            
            case 1.0f:
                moveState = MovementForceStates.Moving;
                movementX = input.x;

                if (LastInputX == input.x)
                {
                    break;
                }
                FlipCharacterSprite();
                LastInputX = input.x;
                break;
        }
    }

    
    
    
    private void OnJump(InputAction.CallbackContext obj)
    {
        if (Grounded)
        { 
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Grounded = false;
            Debug.Log("We Jumped");
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider != top)
        {
            Grounded = true;
            rb.gravityScale = 1;
            Debug.Log("Landed");
        }
    }


    private void FixedUpdate()
    {
        var vAbs = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", vAbs);
        if (moveState == MovementForceStates.Moving 
            && vAbs < movementTopSpeed)
        {
            rb.AddForce(new Vector2(movementX * movementMult , 0), ForceMode2D.Impulse);
        }
        //Debug.Log($"Moving: {movementX}");
        if (rb.velocity.y < -5)
        {
            var newScale = rb.gravityScale + Mathf.Abs(rb.velocity.y) / fallMult;
            rb.gravityScale = Mathf.Clamp(0, fallTopSpeed, newScale);
        }
    }
}
public enum PlayerChoice
{
    Player1,
    Player2,
    Player3,
    Player4
}

public enum MovementForceStates
{
    Idle,
    Moving,
    Climbing,
    Jumping
}