using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class XRButton : MonoBehaviour
{
    public UnityEvent pressed;
    private XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(Selected);
    }

    public void Selected(SelectEnterEventArgs args) {
        interactable.interactionManager.SelectCancel(args.interactorObject.transform.gameObject.GetComponent<IXRSelectInteractor>(), interactable.GetComponent<IXRSelectInteractable>());
        pressed.Invoke();
    }
}
