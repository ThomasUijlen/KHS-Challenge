using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class ThrowableEquipment : Equipment
{
    public float throwForce = 10.0f;
    public void Update() {
        if(grabController == null) return;
        if(grabController.TriggerJustPressed()) {
            interactable.interactionManager.SelectCancel(grabController.GetComponent<IXRSelectInteractor>(), interactable.GetComponent<IXRSelectInteractable>());

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if(rigidbody == null) return;
            rigidbody.AddForce(transform.forward*throwForce, ForceMode.Impulse);
        }
    }
}
