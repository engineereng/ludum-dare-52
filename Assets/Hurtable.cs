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
            yield return new WaitForSeconds(0.3f);
            foreach(Transform t in hurtableObject.transform)
            {
                t.gameObject.tag = "Dead";
            }
            hurtableObject.tag = "Dead";
            hurtableObject.layer = deadLayer;
            sprite.enabled = false;
            yield return new WaitForSeconds(6.0f);
            Die();
        }
        yield return new WaitForSeconds(0.3f);
        sprite.color = defaultColor;
        
    }

    private void Die()
    {
        Destroy(hurtableObject);
        Debug.Log("Dead!");
         // destroy the object after 6 seconds
    }
}
