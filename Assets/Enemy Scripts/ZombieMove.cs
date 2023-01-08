using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove: MonoBehaviour

{
    [SerializeField] protected float attackDamage = 10f;
    public float AttackDamage {
        get {return attackDamage;}
    }

    public float moveSpeed = 1.0f;
    public float radius = 0.5f;
    public Rigidbody2D my_rb; 

    private Collider2D collision;
    private Vector2 currDirection;
    private bool isSoulAttackable;


    void Start()
    {
        my_rb = this.GetComponent<Rigidbody2D>();
        currDirection = transform.up;
        my_rb.velocity = currDirection * moveSpeed;
        
    }

    void FixedUpdate() 
    {
        LayerMask layerMask = LayerMask.GetMask("Souls");
        collision = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collision != null && collision.gameObject.tag == "Soul") {
            // Debug.Log(collision);
            float step = moveSpeed * Time.deltaTime;
            my_rb.velocity = Vector2.zero;
            my_rb.MovePosition(Vector2.MoveTowards(my_rb.position, collision.transform.position, step));
        } else {
            my_rb.velocity = currDirection * moveSpeed;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            currDirection = currDirection * -1.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Soul") 
        {
            Debug.Log("enter attack range");
            isSoulAttackable = true;
            StartCoroutine(AttackSequence(collider.transform.parent.gameObject));
        } 
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Soul") 
        {
            Debug.Log("leaving attack range");
            isSoulAttackable = false;
        } 
    }

    IEnumerator AttackSequence(GameObject soul) {
        Hurtable hurtable = soul.GetComponent<Hurtable>();
        while (isSoulAttackable) {
            Debug.Log("Hit " + soul);
            yield return new WaitForSeconds(1.0f);
            hurtable.TakeDamage(AttackDamage);
            if (hurtable.isDead) {
                isSoulAttackable = false;
            }
        }
        
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
