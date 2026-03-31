using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    public event Action OnMovementPerformed;
    public event Action OnMovementCanceled;
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnDeath; 
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float gravity = -25f;
    
    [Header("Movement Settings")]
    [SerializeField] private InputReader input;
    [SerializeField] private Transform model;
    
    [Header("Prefabs")]
    [SerializeField] private ProjectileType projectileType;

    [SerializeField] private Transform _projectileSpawnTransform;
    
    private CharacterController _controller;
    private Vector2 _inputMove;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _canMoveX = true;
    private bool _canMoveZ = false;
    private MoverAllower _moverAllower = null;
    
    private ObjectPool<Projectile> _projectilePool;
    private GameManager _gameManager;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _gameManager = FindObjectOfType<GameManager>();
        input.OnMovePerformed += OnMovememtPerformed;
        input.OnMoveCanceled += OnMoveCanceled;
        input.OnJumpPerformed += OnJumpPerformed;
        input.OnAttackPerformed += OnAttackPerformed;
        CreateBulletPool();
    }

    private void OnDisable()
    {
        input.OnMovePerformed -= OnMovememtPerformed;
        input.OnMoveCanceled -= OnMoveCanceled;
        input.OnJumpPerformed -= OnJumpPerformed;
        input.OnAttackPerformed -= OnAttackPerformed;
    }

    private void Update()
    {
        if (_gameManager.CurrentGameState != GameManager.GameState.Playing) return;
        HandleMovementVelocity();
        ApplyMovement();
        ApplyGravity();
        FlipModel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MoverAllower zone))
        {
            _moverAllower = zone;
            _canMoveX = zone.CanMoveX;
            _canMoveZ = zone.CanMoveZ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MoverAllower zone) && _moverAllower == zone)
        {
            _moverAllower = null;
            _canMoveX = true;
            _canMoveZ = false;
            
            _velocity.z = 0;
            var difference1 = zone.Limits[0].position - this.transform.position;
            var difference2 = zone.Limits[1].position - this.transform.position;
            _controller.enabled = false;
            var fixedPosition = Vector3.zero;
            if (difference1.magnitude < difference2.magnitude)
            {
                fixedPosition = new Vector3(transform.position.x, transform.position.y, zone.Limits[0].position.z);
            }
            else
            {
                fixedPosition = new Vector3(transform.position.x, transform.position.y, zone.Limits[1].position.z);
            }
            transform.SetPositionAndRotation(fixedPosition, transform.rotation);
            _controller.enabled = true;
        }
    }

    private void OnMovememtPerformed(Vector2 movementDirection)
    {
        _inputMove = movementDirection;
        OnMovementPerformed?.Invoke();
    }

    private void OnMoveCanceled(Vector2 movementDirection)
    {
        _inputMove = movementDirection;
        OnMovementCanceled?.Invoke();
    }

    private void OnJumpPerformed()
    {
        if (_controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            OnJump?.Invoke();
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
        float currentSpeed = moveSpeed;
        Vector3 desiredMove = Vector3.zero;

        var moveX = 1f;
        var moveZ = 0f;
        if (_moverAllower != null)
        {
            moveX = 1;
            moveZ = _canMoveZ ? 1 : 0f;
        }
        desiredMove.x = _inputMove.x * currentSpeed * moveX;
        desiredMove.z = _inputMove.y * currentSpeed * moveZ;   
        _velocity.x = desiredMove.x;
        _velocity.z = desiredMove.z;
        
    }

    private void ApplyMovement()
    {
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void FlipModel()
    {
        if (model == null) return;

        Vector3 moveDir = new Vector3(_inputMove.x, 0f, _inputMove.y);

        if (moveDir.sqrMagnitude > 0.001f)
        {
            model.right = moveDir.normalized;
        }
    }
    
    private void OnAttackPerformed()
    {
        Projectile newProjectile = _projectilePool.Get();
        newProjectile.Init(this, bulletSpawnPosition:  _projectileSpawnTransform.position, this.transform.right);
        OnAttack?.Invoke();
    }
    
    private void CreateBulletPool()
    {
        _projectilePool = new ObjectPool<Projectile>(
            () => Instantiate(projectileType.ProjectilePrefab.GetComponent<Projectile>()),
            projectile => { projectile.gameObject.SetActive(true);},
            projectile => { projectile.gameObject.SetActive(false);},
            projectile => { Destroy(projectile.gameObject);},
            false, // collection checking is not needed here and saves CPU
            20,
            40
        );
    }
    public void ReleaseBulletPool(Projectile projectile) => _projectilePool.Release(projectile);

    public void Die()
    {
        OnDeath?.Invoke();
    }
    
}
