using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : IDisposable
{
    private readonly ActionsMap actions;

    public Action<Vector2> MovePerformed;
    public Action MoveCancelled;
    public Action<bool> IsMove;
    public Action WeaponSwitched;

    private InputManager()
    {
        actions = new ActionsMap();

        actions.Player.Enable();

        actions.Player.Move.performed += OnMovePerformed;
        actions.Player.Move.started += OnMoveStarted;
        actions.Player.Move.canceled += OnMoveCanceled;
        actions.Player.SwitchWeapon.performed += OnWeaponSwitched;
    }

    private void OnWeaponSwitched(InputAction.CallbackContext obj)
    {
        WeaponSwitched.Invoke();
    }

    private void OnMoveStarted(InputAction.CallbackContext obj)
    {
        IsMove.Invoke(true);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        IsMove.Invoke(false);

    }


    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        Vector2 moveDirection = obj.ReadValue<Vector2>();
        MovePerformed.Invoke(moveDirection);
    }

    public void Dispose()
    {
        actions.Player.Disable();
        actions.Player.Move.performed -= OnMovePerformed;
        actions.Player.Move.started -= OnMoveStarted;
        actions.Player.Move.canceled -= OnMoveCanceled;
        actions.Player.SwitchWeapon.performed -= OnWeaponSwitched;
    }
}
