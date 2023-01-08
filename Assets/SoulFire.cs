using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFire : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D collision) 
    {
        Debug.Log("Collision!");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Souls"))
        {
            Debug.Log("Killed soul");
            Destroy(collision.gameObject); // fry the soul
            // TODO: add animation of soul burning? Maybe dramatic explosion
        }
    }
}
