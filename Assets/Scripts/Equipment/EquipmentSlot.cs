using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public string slotName;
    private Equipment currentEquipment;
    private Rigidbody body;

    void Update()
    {
        if(!isFull()) return;
        currentEquipment.transform.position = transform.position;
        currentEquipment.transform.rotation = transform.rotation;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    public void Hold(Equipment equipment) {
        if(isFull()) return;
        Rigidbody rigidbody = equipment.GetComponent<Rigidbody>();
        if(rigidbody == null) return;
        currentEquipment = equipment;
        body = rigidbody;
    }

    public void Release(Equipment equipment) {
        Rigidbody rigidbody = equipment.GetComponent<Rigidbody>();
        if(rigidbody == null) return;
        rigidbody.WakeUp();
        currentEquipment = null;
        body = null;
    }

    public bool isFull() {
        return currentEquipment != null;
    }

    public Equipment GetCurrentEquipment() {
        return currentEquipment;
    }
}
