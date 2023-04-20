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
        return trigger.action.IsPressed();
    }

    public bool PrimaryPressed() {
        return primary.action.IsPressed();
    }

    public bool SecondaryPressed() {
        return secondary.action.IsPressed();
    }
}
