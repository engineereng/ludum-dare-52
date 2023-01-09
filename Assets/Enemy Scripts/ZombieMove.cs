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
    public bool isMovementSideways;
    private Collider2D collision;
    private Vector2 currDirection;
    private bool isSoulNearby;

    public Animator animator;
    private float delay = 0.3f;
    private bool attackBlocked;
    bool isMovementPressed;
     
    void Start()
    {
        my_rb = this.GetComponent<Rigidbody2D>();
        if (isMovementSideways) {
            currDirection = transform.right;
        } else {
            currDirection = transform.up;
        }
        
        my_rb.velocity = currDirection * moveSpeed;
        
    }
    void Update()
    {
        if (my_rb.velocity.x == 0 && my_rb.velocity.y == 0)
        {
            isMovementPressed = false;
        }
        if (my_rb.velocity.x == 1 || my_rb.velocity.y == 1 || my_rb.velocity.x == -1 || my_rb.velocity.y == -1)
        {
            isMovementPressed = true;
        }
        handleAnimation();
    }
    void FixedUpdate() 
    {
        if(my_rb.velocity == Vector2.zero) {
            my_rb.velocity = currDirection * moveSpeed;
        }
        LayerMask layerMask = LayerMask.GetMask("Souls");
        collision = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collision != null && collision.gameObject.tag == "Soul") {
            float step = moveSpeed * Time.deltaTime;
            my_rb.velocity = Vector2.zero;
            my_rb.MovePosition(Vector2.MoveTowards(my_rb.position, collision.transform.position, step));
        } else {
            my_rb.velocity = currDirection * moveSpeed;
        } 
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("collided with something");
        if (collision.gameObject.tag == "Obstacles")
        {
            Debug.Log("collided with wall");
            currDirection = currDirection * -1.0f;
        }
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Soul" && !isSoulNearby) 
        {
            Debug.Log("enter attack range");
            isSoulNearby = true;
            StartCoroutine(AttackSequence(collider.transform.parent.gameObject));
        } 
    }
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Soul") 
        {
            Debug.Log("leaving attack range");
            isSoulNearby = false;
        } 
    }
    IEnumerator AttackSequence(GameObject soul) {
        Debug.Log("Hit Sequence Started");
        Hurtable hurtableSoul = soul.GetComponent<Hurtable>();
        while (soul.tag == "Soul") {
            if (soul.tag == "Soul") {
                yield return new WaitForSeconds(1.0f);
                Debug.Log("Hit " + soul);
                hurtableSoul.TakeDamage(AttackDamage);
            } else if (soul.tag == "Dead") {
                isSoulNearby = false;
            }
        }
        yield return new WaitForEndOfFrame();
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Gizmos.DrawSphere(transform.position, radius);
    }

    void Attack()
    {
        if (attackBlocked)
        {
            return;
        }
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());

    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    void handleAnimation()
    {
        bool Up = animator.GetBool("Up");
        bool Down = animator.GetBool("Down");
        bool Left = animator.GetBool("Left");
        bool Right = animator.GetBool("Right");
        //bool stopMoving = animator.GetBool("stopMoving");
        //bool onClick = animator.GetBool("onClick");

        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
       

        if (isMovementPressed && (my_rb.velocity.x == 0) && (my_rb.velocity.y == 1))
        {
            animator.SetBool("Up", true);

            if (collision != null && collision.gameObject.tag == "Soul")
            {
                Attack();
            }

        }
        else if (isMovementPressed && (my_rb.velocity.x == 0) && (my_rb.velocity.y == -1))
        {
            animator.SetBool("Down", true);
            if (collision != null && collision.gameObject.tag == "Soul")
            {
                Attack();
            }
        }
        else if (isMovementPressed && (my_rb.velocity.x == 1) && (my_rb.velocity.y == 0))
        {
            animator.SetBool("Right", true);
            if (collision != null && collision.gameObject.tag == "Soul")
            {
                Attack();
            }
        }
        else if (isMovementPressed && (my_rb.velocity.x == -1) && (my_rb.velocity.y == 0))
        {
            animator.SetBool("Left", true);
            if (collision != null && collision.gameObject.tag == "Soul")
            {
                Attack();
            }
        }

    }
}