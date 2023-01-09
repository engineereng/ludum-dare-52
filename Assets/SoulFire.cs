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
            collision.gameObject.TryGetComponent<Hurtable>(out Hurtable component);
            Debug.Log("Killed soul");
            component.TakeDamage(10000);
            // Destroy(collision.gameObject); // fry the soul
            // TODO: add animation of soul burning? Maybe dramatic explosion
        }
    }
}
