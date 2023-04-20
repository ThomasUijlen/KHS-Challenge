using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioEffect))]
public class AmmoHandler : MonoBehaviour
{
    public string ammoName = "";
    public Ammo ammoPrefab;

    private MeshRenderer meshRenderer;
    private AudioEffect audioEffect;
    private int currentAmmo = -1;
    private float ejectTime = -1f;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        audioEffect = GetComponent<AudioEffect>();
        UpdateMesh();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if(currentAmmo > 0) return;
        Ammo ammo = collisionInfo.gameObject.GetComponent<Ammo>();
        if(ammo == null) return;
        if(ammo.ammoName != ammoName) return;
        if(Time.time - ejectTime < 1.0f) return;

        currentAmmo = ammo.currentAmmo;
        audioEffect.Play();
        UpdateMesh();
        Destroy(ammo.gameObject);
    }

    public void EjectAmmo() {
        if(currentAmmo < 0) return;

        Ammo ammo = Instantiate(ammoPrefab, transform.position, transform.rotation);
        ammo.currentAmmo = currentAmmo;
        ejectTime = Time.time;
        currentAmmo = -1;
        audioEffect.Play(-0.2f);
        UpdateMesh();
    }

    public int GetAmmoCount() {
        return currentAmmo;
    }

    public void ConsumeAmmo() {
        if(currentAmmo > 0) currentAmmo -= 1;
    }

    private void UpdateMesh() {
        meshRenderer.enabled = currentAmmo >= 0;
    }
}
