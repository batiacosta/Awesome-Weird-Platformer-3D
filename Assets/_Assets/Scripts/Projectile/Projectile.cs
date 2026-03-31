using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 10f;
    
    private Vector3 _fireDirection;
    private Rigidbody _rigidBody;
    private PlayerController _playerController;

    public void Init(PlayerController playerController, Vector3 bulletSpawnPosition, Vector3 transformRight)
    {
        _playerController = playerController;
        transform.SetPositionAndRotation(bulletSpawnPosition, Quaternion.identity);
        _fireDirection = playerController.transform.right;
    }
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * moveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        _playerController.ReleaseBulletPool(this);
    }
}
