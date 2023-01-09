using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    
    [SerializeField] public float health = 100;
    public GameObject hurtableObject;
    public SpriteRenderer sprite;
    private Color defaultColor;
    private int deadLayer;
    
    void Start() 
    {
        defaultColor = sprite.color;
        deadLayer = LayerMask.NameToLayer("Dead");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(this.name + " was hurt for " + damage);
        health -= damage;
        StartCoroutine(HurtCoroutine());
    }

    private IEnumerator HurtCoroutine(){
        sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.7f);
        if (health <= 0) {
            hurtableObject.tag = "Dead";
            hurtableObject.layer = deadLayer;
            yield return new WaitForSeconds(0.3f);
            Die();
        }
        yield return new WaitForSeconds(0.3f);
        sprite.color = defaultColor;
        
    }

    private void Die()
    {
        Debug.Log("Dead!");
        Destroy(hurtableObject, 6.0f); // destroy the object after 6 seconds
        hurtableObject.SetActive(false); // disable the object
    }
}
