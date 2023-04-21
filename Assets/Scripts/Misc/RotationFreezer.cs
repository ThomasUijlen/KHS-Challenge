using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFreezer : MonoBehaviour
{
    public bool freezeX = false;
    public bool freezeY = false;
    public bool freezeZ = false;

    void FixedUpdate()
    {
        Quaternion rotation = transform.rotation;
        if(freezeX) rotation.x = 0;
        if(freezeY) rotation.y = 0;
        if(freezeZ) rotation.z = 0;
        transform.rotation = rotation;
    }
}
