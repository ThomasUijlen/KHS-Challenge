using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRInput : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty trigger;
    [SerializeField]
    private InputActionProperty primary;
    [SerializeField]
    private InputActionProperty secondary;

    public bool TriggerPressed() {
        return IsPressed(trigger.action);
    }

    public bool TriggerJustPressed() {
        return JustPressed(trigger.action);
    }

    public bool TriggerJustReleased() {
        return JustReleased(trigger.action);
    }

    public bool PrimaryPressed() {
        return primary.action.IsPressed();
    }

    public bool PrimaryJustPressed() {
        return JustPressed(primary.action);
    }

    public bool PrimaryJustReleased() {
        return JustReleased(primary.action);
    }

    public bool SecondaryPressed() {
        return secondary.action.IsPressed();
    }

    public bool SecondaryJustPressed() {
        return JustPressed(secondary.action);
    }

    public bool SecondaryJustReleased() {
        return JustReleased(secondary.action);
    }

    private bool IsPressed(InputAction action) {
        return action.IsPressed();
    }

    private bool JustPressed(InputAction action) {
        return action.triggered && action.ReadValue<float>() > 0;
    }

    private bool JustReleased(InputAction action) {
        return action.triggered && action.ReadValue<float>() == default;
    }
}
