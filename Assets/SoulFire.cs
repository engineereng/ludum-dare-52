using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFire : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.TryGetComponent<IsSoul>(out var IsSoul))
        {
            Debug.Log("Killed soul: " + IsSoul.isSoul);
            if (IsSoul.isSoul) Destroy(other.gameObject); // fry the soul
        }
    }
}
