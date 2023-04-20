using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class XRInput : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty trigger;
    [SerializeField]
    private InputActionProperty primary;
    [SerializeField]
    private InputActionProperty secondary;
    [SerializeField]
    private InputActionProperty rumble;

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
        return IsPressed(primary.action);
    }

    public bool PrimaryJustPressed() {
        return JustPressed(primary.action);
    }

    public bool PrimaryJustReleased() {
        return JustReleased(primary.action);
    }

    public bool SecondaryPressed() {
        return IsPressed(secondary.action);
    }

    public bool SecondaryJustPressed() {
        return JustPressed(secondary.action);
    }

    public bool SecondaryJustReleased() {
        return JustReleased(secondary.action);
    }

    public void Rumble(float amplitude, float duration)
    {
        if (rumble.action?.activeControl?.device is XRControllerWithRumble rumbleController)
        {
            rumbleController.SendImpulse(amplitude, duration);
        }
    }

    private bool IsPressed(InputAction action) {
        return action.IsPressed();
    }

    private bool JustPressed(InputAction action) {
        return action.triggered && action.ReadValue<float>() > 0;
    }

    private bool JustReleased(InputAction action) {
        return action.triggered && action.ReadValue<float>() == 0;
    }
}
