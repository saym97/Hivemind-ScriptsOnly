using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public float GapBetweenButtons;
    public AbilityButton ButtonPrefab;
    public AbilityHub AbilityHub;
    public AbilityEvent OnSelectEvent;
    [HideInInspector] public float ButtonOffest;
    [HideInInspector] public List<AbilityButton> m_AllButtons = new List<AbilityButton>();
    [SerializeField] private bool m_IsGamepadActive;
    Vector2 Screen = Vector2.zero;

    void Start()
    {
        ButtonOffest = 360f / AbilityHub.AllAbilities.Length;
        InputUser.onChange += DeviceChange;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        bool IsPressed;
        if (m_IsGamepadActive)
        {
            Screen = Gamepad.current.rightStick.ReadValue() ;
        }
        else
        {
            Screen = Mouse.current.position.ReadValue();
            Screen = Screen - position;
        }
        //Debug.Log("Screen Position " +  Screen);
        var mouseAngle = NormalizeAngle((ButtonOffest / 2f) -
                                        Vector2.SignedAngle(Vector2.up, Screen));
        // Debug.Log( " Screen mouse" + mouseAngle);
        var activeElement = (int) (mouseAngle / ButtonOffest);
       // if (Mouse.current.rightButton.isPressed || Gamepad.current.rightTrigger.isPressed)
        //{
            //Debug.Log("ActiveElement:" + activeElement);
            // Debug.Log("Ability Name: " + m_AllButtons[activeElement].AbilityAsset.Name);
            if (!m_AllButtons[activeElement].IsSelected)
                EventSystem.current.SetSelectedGameObject(m_AllButtons[activeElement].gameObject);
        //}
    }

    private float NormalizeAngle(float angle) => (angle + 360f) % 360f;

    public void CreateButtons()
    {
        AbilityButton head = null;
        AbilityButton temp = null;
        DeleteAllButtons();
        ButtonOffest = 360f / AbilityHub.AllAbilities.Length;
        for (int i = 0; i < AbilityHub.AllAbilities.Length; i++)
        {
            AbilityButton button = Instantiate(ButtonPrefab, this.transform);
            if (head)
            {
                temp.Next = button;
                button.Previous = temp;
                if (i != AbilityHub.AllAbilities.Length - 1) temp = button;
                else
                {
                    button.Next = head;
                    head.Previous = button;
                }
            }
            else
            {
                head = button;
                temp = button;
            }

            button.transform.localPosition = Vector3.zero;
            button.transform.localRotation = Quaternion.identity;
            button.AbilityAsset = AbilityHub.AllAbilities[i];

            button.image.fillAmount = 1f / AbilityHub.AllAbilities.Length - GapBetweenButtons / 360f;

            button.image.transform.localPosition = Vector3.zero;
            button.image.transform.localRotation =
                Quaternion.Euler(0, 0, GapBetweenButtons / 2f + ButtonOffest / 2f - i * ButtonOffest);
            button.RadialMenu = this;
            button.SetData();
            m_AllButtons.Add(button);
        }
    }

    //Clears the Array and Destroys the AbilityButton GameObjects in EditorMode.
    public void DeleteAllButtons()
    {
        m_AllButtons.Clear();
        for (int i = this.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
    }

    private void DeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            var scheme = user.controlScheme.Value.name;
            Debug.Log("Scheme: "  + scheme);
            if (scheme.Equals("Gamepad")) m_IsGamepadActive = true;
            else m_IsGamepadActive = false;
        }
    }

   // private void OnEnable()=> InputUser.onChange += DeviceChange;
    //private void OnDisable() => InputUser.onChange -= DeviceChange;
}