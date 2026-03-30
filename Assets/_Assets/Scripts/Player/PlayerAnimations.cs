using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerController _playerController;
    private Animator _animator;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _animator = GetComponentsInChildren<Animator>()[0];
        _playerController.OnMovementPerformed += OnMovementPerformed;
        _playerController.OnMovementCanceled += OnMovementCanceled;
        _playerController.OnAttack += OnAttack;
        _playerController.OnJump += OnJump;
    }

    private void OnDisable()
    {
        _playerController.OnMovementPerformed -= OnMovementPerformed;
        _playerController.OnMovementCanceled -= OnMovementCanceled;
        _playerController.OnAttack -= OnAttack;
        _playerController.OnJump -= OnJump;
    }

    private void OnMovementPerformed()
    {
        _animator.SetBool("Running", true);
    }

    private void OnMovementCanceled()
    {
        Debug.Log("Movement Canceled");
        _animator.SetBool("Running", false);
    }

    private void OnAttack()
    {
        _animator.SetTrigger("Attack");
    }

    private void OnJump()
    {
        _animator.SetTrigger("Jump");
    }
}
