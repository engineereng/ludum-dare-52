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
    private string compassDirection;

    public Animator animator;
    private float delay = 0.3f;
    private bool attackBlocked;

    bool Up, Down, Left, Right;

    //private string compassDirection;
    
    void Start()
    {
        my_rb = this.GetComponent<Rigidbody2D>();
        if (isMovementSideways) {
            currDirection = transform.right;
        } else {
            currDirection = transform.up;
        }
        
        my_rb.velocity = currDirection * moveSpeed;
        

        Up = animator.GetBool("Up");
        Down = animator.GetBool("Down");
        Left = animator.GetBool("Left");
        Right = animator.GetBool("Right");


        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);

        compassDirection = giveDirection(my_rb.velocity);
        handleAnimationAttackSoul(compassDirection);
        
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
            Vector2 soulPosition = collision.transform.position;
            Vector2 direction = (soulPosition - my_rb.position).normalized;
            compassDirection = giveDirection(direction);
            handleAnimationAttackSoul(compassDirection);
        } else {
            my_rb.velocity = currDirection * moveSpeed;
            compassDirection = giveDirection(my_rb.velocity);
            handleAnimationAttackSoul(compassDirection);
        } 
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("collided with something");
        if (collision.gameObject.tag == "Obstacles")
        {
            Debug.Log("collided with wall");
            currDirection = currDirection * -1.0f;
            compassDirection = giveDirection(my_rb.velocity);
            handleAnimationAttackSoul(compassDirection);
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
        while (soul.tag == "Soul" && this.tag == "Enemy") {
            yield return new WaitForSeconds(1.0f);
            if (soul.tag == "Soul" && this.tag == "Enemy") {
                Debug.Log("Hit " + soul);
                hurtableSoul.TakeDamage(AttackDamage);
                Attack();
            } else if (soul.tag == "Dead" || this.tag == "Dead") {
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

    void handleAnimationAttackSoul(string direction)
    {
        if (direction == "Up")
        {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);

        }
        else if (direction == "Down")
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
        else if (direction == "Right")
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }
        else if (direction == "Left")
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }
    }

    public string giveDirection(Vector2 vector) {
        Vector2 upVector = Vector2.up;
        Vector2 downVector = -Vector2.up;
        Vector2 rightVector = Vector2.right;
        Vector2 leftVector = -Vector2.right;

        Vector2[] compass = {upVector, downVector, rightVector, leftVector};

        float maxDot = -Mathf.Infinity;
        Vector2 ret = Vector2.zero;

        foreach(Vector2 dir in compass) {
            float t = Vector2.Dot(vector, dir);
            if(t > maxDot) {
                ret = dir;
                maxDot = t;
            }
        }

        if (ret == upVector) {
            return "Up";
        } else if (ret == downVector) {
            return "Down";
        } else if (ret == rightVector) {
            return "Right";
        } else if (ret == leftVector) {
            return "Left";
        } else {
            return "something went wrong";
        }
    }

}