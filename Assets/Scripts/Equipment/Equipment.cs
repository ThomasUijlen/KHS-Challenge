using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

[RequireComponent(typeof(XRGrabInteractable))]
public class Equipment : MonoBehaviour
{
    [Header("Hovering Logic")]
    public GameObject baseMesh;
    public Material hoverMaterial;
    [Header("Equipment Slots")]
    public string[] validSlots;

    protected XRGrabInteractable interactable;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    protected XRInput grabController;
    private EquipmentSlot currentSlot;
    private EquipmentSlot hoverSlot;
    private bool hovered = false;

    public void Start()
    {
        //Connect relevant Events
        interactable = GetComponent<XRGrabInteractable>();
        interactable.hoverEntered.AddListener(Hovered);
        interactable.hoverExited.AddListener(Unhovered);
        interactable.selectEntered.AddListener(Selected);
        interactable.selectExited.AddListener(Deselected);

        if(baseMesh != null) {
            //Add a new GameObject for the hover mesh
            GameObject hoverObject = new GameObject("HoverMesh");
            hoverObject.transform.SetParent(baseMesh.transform);
            hoverObject.transform.localPosition = Vector3.zero;
            hoverObject.transform.localRotation = Quaternion.identity;
            hoverObject.transform.localScale = Vector3.one;

            meshFilter = hoverObject.AddComponent<MeshFilter>();
            meshRenderer = hoverObject.AddComponent<MeshRenderer>();
            meshFilter.mesh = baseMesh.GetComponent<MeshFilter>().mesh;

            //Apply hoverMaterial to all surfaces
            Material[] materials = baseMesh.GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = hoverMaterial;
            }
            meshRenderer.materials = materials;

            UpdateHoverMesh();
        }
    }

    public bool IsGrabbed() {
        return grabController != null;
    }

    public virtual void Hovered(HoverEnterEventArgs args) {
        hovered = true;
        UpdateHoverMesh();
    }

    public virtual void Unhovered(HoverExitEventArgs args) {
        hovered = false;
        UpdateHoverMesh();
    }

    public virtual void Selected(SelectEnterEventArgs args) {
        grabController = args.interactorObject.transform.gameObject.GetComponent<XRInput>();
        UpdateHoverMesh();
        
        if(currentSlot != null) {
            currentSlot.Release(this);
            currentSlot = null;
        }
    }

    public virtual void Deselected(SelectExitEventArgs args) {
        grabController = null;
        UpdateHoverMesh();
        
        if(hoverSlot && !hoverSlot.isFull()) {
            currentSlot = hoverSlot;
            currentSlot.Hold(this);
        }
    }

    private void UpdateHoverMesh() {
        if(baseMesh == null) return;
        meshRenderer.enabled = hovered && grabController == null;
    }

    void OnTriggerEnter(Collider other)
    {
        EquipmentSlot slot = other.GetComponent<EquipmentSlot>();
        if(slot == null) return;
        if(hoverSlot != null) return;
        if(!Array.Exists(validSlots, slotName => slotName == slot.slotName)) return;
        hoverSlot = slot;
    }

    void OnTriggerExit(Collider other)
    {
        EquipmentSlot slot = other.GetComponent<EquipmentSlot>();
        if(slot == null) return;
        if(slot == hoverSlot) hoverSlot = null;
    }
}
