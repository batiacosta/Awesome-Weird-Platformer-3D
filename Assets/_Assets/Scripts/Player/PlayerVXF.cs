using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVXF : MonoBehaviour
{
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _playerController.OnAttack += OnAttack;
    }

    private void OnDisable()
    {
        _playerController.OnAttack += OnAttack;
    }

    private void OnAttack()
    {
        // Show Particles.
    }
}
