using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public string ammoName = "";
    public int maxAmmo = 20;
    public int currentAmmo;

    void Awake()
    {
        currentAmmo = maxAmmo;
    }
}
