using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    [SerializeField] public float health = 100;
    public GameObject hurtableObject;
    public SpriteRenderer sprite;
    public bool isDead;
    private Color defaultColor;
    private int frame;

    void Start() 
    {
        defaultColor = sprite.color;
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
            isDead = true;
            yield return new WaitForSeconds(0.8f);
            Die();
        }
        yield return new WaitForSeconds(0.3f);
        sprite.color = defaultColor;
        
    }

    private void Die()
    {
        Debug.Log("Dead!");
        Destroy(hurtableObject);
        
        // TODO 
        // play some death animation
        // disable the entity
    }
}
