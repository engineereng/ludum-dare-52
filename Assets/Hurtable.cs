using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    [SerializeField] private float health = 100;

    public void TakeDamage(float damage)
    {
        Debug.Log(this.name + " was hurt for " + damage);
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Dead!");
        
        // TODO 
        // play some death animation
        // disable the entity
    }
}
