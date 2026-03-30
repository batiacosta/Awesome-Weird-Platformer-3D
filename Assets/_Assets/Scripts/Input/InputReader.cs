using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/Input/InputReader")]
public class InputReader : ScriptableObject
{
    public event Action<Vector2> OnMovePerformed;
    public event Action<Vector2> OnMoveCanceled;
    public event Action OnAttackPerformed;
    public event Action OnJumpPerformed; 

    private PlayerInputActions _inputActions;
    

    private void OnEnable()
    {
        if (_inputActions == null) _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Player.Move.performed += MovePerformed;
        _inputActions.Player.Move.canceled += MoveCanceled;
        _inputActions.Player.Attack.performed += AttackPerformed;
        _inputActions.Player.Jump.performed += JumpPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= MovePerformed;
        _inputActions.Player.Move.canceled -= MoveCanceled;
        _inputActions.Player.Attack.performed -= AttackPerformed;
        _inputActions.Player.Jump.performed -= JumpPerformed;
        _inputActions.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        OnMovePerformed?.Invoke(obj.ReadValue<Vector2>());
    }

    private void MoveCanceled(InputAction.CallbackContext obj)
    {
        OnMoveCanceled?.Invoke(Vector2.zero);
    }

    private void AttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttackPerformed?.Invoke();
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        OnJumpPerformed?.Invoke();
    }
}
