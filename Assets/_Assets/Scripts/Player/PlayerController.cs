using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float gravity = -25f;
    
    [Header("Movement Settings")]
    [SerializeField] private InputReader input;
    [SerializeField] private Transform model;
    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float crouchHeight = 1f;
    
    [Header("References")]
    private CharacterController _controller;

    private float _originalHeight;
    private Vector2 _inputMove;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isCrouching;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _originalHeight = _controller.height;
    }

    private void OnEnable()
    {
        input.OnMovePerformed += OnMovememtPerformed;
        input.OnMoveCanceled += OnMoveCanceled;
        input.OnJumpPerformed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        input.OnMovePerformed += OnMovememtPerformed;
        input.OnMoveCanceled += OnMoveCanceled;
        input.OnJumpPerformed += OnJumpPerformed;
    }

    private void Update()
    {
        HandleMovementVelocity();
        ApplyMovement();
        ApplyGravity();
    }

    private void OnMovememtPerformed(Vector2 movementDirection)
    {
        _inputMove = movementDirection;
    }

    private void OnMoveCanceled(Vector2 movementDirection)
    {
        _inputMove = movementDirection;
    }

    private void OnJumpPerformed()
    {
        if (_controller.isGrounded && !_isCrouching)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _velocity.y += gravity * Time.deltaTime;
        }else if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }
    private void HandleMovementVelocity()
    {
        var currentSpeed = _isCrouching ? crouchSpeed : moveSpeed;
        var moveDirection = new Vector3(_inputMove.x, _inputMove.y, 0);
        _velocity.x = moveDirection.x * currentSpeed;
    }

    private void ApplyMovement()
    {
        _controller.Move(_velocity * Time.deltaTime);
    }
    
}
