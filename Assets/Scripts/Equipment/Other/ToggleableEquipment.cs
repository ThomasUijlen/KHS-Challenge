using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ToggleableEquipment : Equipment
{
    public enum BUTTON {
        TRIGGER,
        PRIMARY,
        SECONDARY,
    }

    public bool toggled = false;
    public BUTTON button;

    private Animator animator;

    new public void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        UpdateAnimator();
    }

    public void Update() {
        if(grabController == null) return;

        switch(button) {
            case BUTTON.TRIGGER:
            if(grabController.TriggerJustPressed()) Toggle();
            break;
            case BUTTON.PRIMARY:
            if(grabController.PrimaryJustPressed()) Toggle();
            break;
            case BUTTON.SECONDARY:
            if(grabController.SecondaryJustPressed()) Toggle();
            break;
        }
    }

    public void Toggle() {
        toggled = !toggled;
        UpdateAnimator();
    }

    private void UpdateAnimator() {
        if(toggled) {
            animator.Play("On");
        } else {
            animator.Play("Off");
        }
    }
}
