using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

[RequireComponent(typeof(XRGrabInteractable))]
public class Equipment : MonoBehaviour
{
    [Header("Hovering Logic")]
    public GameObject baseMesh;
    public Material hoverMaterial;

    private XRGrabInteractable interactable;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private XRInput grabController;
    private bool hovered = false;

    void Start()
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

    public void Update() {
        if(grabController == null) return;
        meshRenderer.enabled = false;
        if(grabController.TriggerPressed()) meshRenderer.enabled = true;
        if(grabController.PrimaryPressed()) meshRenderer.enabled = true;
        if(grabController.SecondaryPressed()) meshRenderer.enabled = true;
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
    }

    public virtual void Deselected(SelectExitEventArgs args) {
        grabController = null;
        UpdateHoverMesh();
    }

    public void UpdateHoverMesh() {
        if(baseMesh == null) return;
        meshRenderer.enabled = hovered && grabController == null;
    }
}
