using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected float attackDamage = 10f;
    public float AttackDamage {
        get {return attackDamage;}
    }

    public LayerMask enemyLayers;

    //public Animator animator; // TODO add attack sprite
    public Transform attackPoint;
    public float attackRange;
    private float hitDelta = 0.5f; // time between hits
    private float nextHit = 0f; // time when next hit is available
    private float myTime = 0.0f;
    private bool currentlyHitting;

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Attack") && myTime > nextHit) {
            nextHit = myTime + hitDelta;
            Attack();
            nextHit = nextHit - myTime;
            myTime = 0.0f;
        }
    }

    void Attack()
    {
        currentlyHitting = false;
        Debug.Log("Attempted attack!");
        // TODO animation
        // animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (!currentlyHitting) {
                Debug.Log("Hit " + enemy.name);
                Hurtable hurtable = enemy.GetComponent<Hurtable>();
                hurtable.TakeDamage(attackDamage);
                currentlyHitting = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
