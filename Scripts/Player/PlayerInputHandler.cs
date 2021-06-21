using QFSW.QC;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputHandler : MonoBehaviour
{
    private PrototypeInputActions m_inputActions;
    private PlayerContext m_playerContext;

    private bool m_consoleActive = false;

    private void Awake()
    {
        m_inputActions = new PrototypeInputActions();
        m_playerContext = GetComponent<PlayerContext>();

        m_inputActions.Player.Move.performed += ctx => m_playerContext.MovementInput = ctx.ReadValue<Vector2>();
        m_inputActions.Player.BasicAttack.performed += _ => m_playerContext.IsBasicAttack = true;
        m_inputActions.Player.Dash.performed += _ => m_playerContext.IsDash = true;
        m_inputActions.Player.MousePosition.performed += ctx => m_playerContext.MousePosition = ctx.ReadValue<Vector2>();

        m_inputActions.QC.Toogle.performed += _ => ToogleConsole();
        m_inputActions.QC.Submit.performed += _ => QuantumConsole.Instance.InvokeCommand();
        m_inputActions.QC.AutoComplete.performed += _ => QuantumConsole.Instance.ProcessAutocomplete();

        m_inputActions.Player.SpecialAbility.performed += ct => m_playerContext.AbilityAttack = true;
        m_inputActions.Player.SpecialAbility.canceled += ctx => m_playerContext.AbilityAttack = false;

        m_inputActions.Player.ToggleRadialMenu.started += ctx => m_playerContext.ToggleRadialMenu(true);
        m_inputActions.Player.ToggleRadialMenu.canceled += ctx => m_playerContext.ToggleRadialMenu(false);
    }

    private void ToogleConsole()
    {
        m_consoleActive = !m_consoleActive;

        if (m_consoleActive) m_inputActions.Player.Disable();
        else m_inputActions.Player.Enable();

        QuantumConsole.Instance.Activate();
    }

    private void DeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            var scheme = user.controlScheme.Value.name;
            if (scheme.Equals("Gamepad")) m_playerContext.IsGamepadActive = true;
            else m_playerContext.IsGamepadActive = false;
            InputEvents.InputDeviceChange(scheme);
        }
    }

    private void OnEnable()
    {
        m_inputActions.Enable();
        InputUser.onChange += DeviceChange;
    }

    private void OnDisable()
    {
        m_inputActions.Disable();
        InputUser.onChange -= DeviceChange;
    }
}
