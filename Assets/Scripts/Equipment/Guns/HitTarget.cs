using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitTarget : MonoBehaviour
{
    public UnityEvent<float> hit;

    public void Damage(float damage) {
        hit.Invoke(damage);
    }
}
