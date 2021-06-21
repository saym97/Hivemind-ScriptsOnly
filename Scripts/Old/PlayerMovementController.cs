using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    enum MovementAngle
    {
        _45_DEGREE = 1,
        _30_DEGREE = 2
    }
    // Start is called before the first frame update


    [SerializeField]
    public CharacterController m_CharacterController;
    [SerializeField]
    private Vector3 m_rawInputMovement;
    [Header("VALUES TO TWEAK FOR EXPERIMENTATION")]
    public float MovementSpeed;
    [Tooltip("Checking this boolean enables diagonal movement with WASD")]
    public bool IsDiagonalMovement;
    [SerializeField]
    [Tooltip("Set 1 for 45� movement and 2 for 30� movement ")]
    MovementAngle MovementMode = MovementAngle._45_DEGREE;

    public Canvas RadialMenu;
    public AbilityEvent Event;
    private AbilityParent m_CurrentAbility;
    private void OnEnable()
    {
        Event.EventAction += SetCurrentAbiilty;
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        if (IsDiagonalMovement)
            DiagonalWASD(inputMovement);
        else
            CartesianWASD(inputMovement);

        m_rawInputMovement.Normalize();
    }

    void SetCurrentAbiilty(AbilityParent ability)
    {
        m_CurrentAbility = ability;
    }
public void OnToggleRadialMenu(InputAction.CallbackContext value)
    {
       
        if (value.started)
        {
            Debug.Log("Tab Pressed");
            RadialMenu.gameObject.SetActive(true);
        }
        else if (value.canceled)
        {
            Debug.Log("Tab Released");
            RadialMenu.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void DiagonalWASD(Vector2 inputMovement)
    {
        m_rawInputMovement.x = (inputMovement.x + inputMovement.y);
        m_rawInputMovement.z = ((m_rawInputMovement.x / 2 - inputMovement.x) * 2) / (float)MovementMode;
    }

    void CartesianWASD(Vector2 inputMovement)
    {
        //Debug.Log("X: " + inputMovement.x + " Y: " + inputMovement.y);
        if (inputMovement.x != 0 && inputMovement.y != 0)
        {
            inputMovement.y /= (float)MovementMode;
        }
        m_rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
    void MoveThePlayer()
    {
        if (m_rawInputMovement.sqrMagnitude > 0.01f)
        {
            /*Quaternion rotation = Quaternion.Slerp(transform.rotation,
                                                   Quaternion.LookRotation(m_rawInputMovement),
                                                   RotationSpeed * Time.deltaTime);*/
            transform.rotation = Quaternion.LookRotation(m_rawInputMovement);
        }
        m_CharacterController.SimpleMove(m_rawInputMovement * MovementSpeed);
    }
}
