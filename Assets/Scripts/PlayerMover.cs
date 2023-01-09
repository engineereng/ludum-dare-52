using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    
    public Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;

    bool isMovementPressed;


    public enum Facing {Right, Up, Left, Down};
    public Facing facing; // 0 = right, 1 = up, 2 = left, 3 = down

    public float movementSpeed = .5f;

    public Rigidbody2D rb;
    public Transform attackPoint;
    public Transform player;

    private float delay = 0.3f;
    private bool attackBlocked;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        
        rb.freezeRotation = true;
    }
    // Update is called once per frame
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
        bool walkUp = animator.GetBool("walkUp");
        bool walkDown = animator.GetBool("walkDown");
        bool walkLeft = animator.GetBool("walkLeft");
        bool walkRight = animator.GetBool("walkRight");
        //bool stopMoving = animator.GetBool("stopMoving");
        //bool onClick = animator.GetBool("onClick");

        animator.SetBool("walkUp", false);
        animator.SetBool("walkDown", false);
        animator.SetBool("walkLeft", false);
        animator.SetBool("walkRight", false);

        Mouse mouse = Mouse.current;
        
        

        if (isMovementPressed && (currentMovementInput.x == 0) && (currentMovementInput.y == 1))
        {
            animator.SetBool("walkUp", true);
            attackPoint.position = new Vector3(player.position.x, player.position.y + 0.2f, 0f);

            if (mouse.leftButton.wasPressedThisFrame)
            {
                Attack();
            }
            
        }
        else if (isMovementPressed && (currentMovementInput.x == 0) && (currentMovementInput.y == -1))
        {
            animator.SetBool("walkDown", true);
            attackPoint.position = new Vector3(player.position.x, player.position.y - 0.2f, 0f);
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Attack();
            }
        }
        else if (isMovementPressed && (currentMovementInput.x == 1) && (currentMovementInput.y == 0))
        {
            animator.SetBool("walkRight", true);
            attackPoint.position = new Vector3(player.position.x + 0.2f, player.position.y, 0f);
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Attack();
            }
        }
        else if (isMovementPressed && (currentMovementInput.x == -1) && (currentMovementInput.y == 0))
        {
            animator.SetBool("walkLeft", true);
            attackPoint.position = new Vector3(player.position.x - 0.2f, player.position.y, 0f);
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Attack();
            }
        }
        
    }

    void Update()
    {


        currentMovementInput.x = Input.GetAxisRaw("Horizontal"); // L / R movement; [-1, 1]
        currentMovementInput.y = Input.GetAxisRaw("Vertical");
        if (currentMovementInput.x == 0 && currentMovementInput.y == 0) { 
            isMovementPressed = false;
        }
        if (currentMovementInput.x == 1 || currentMovementInput.y == 1 || currentMovementInput.x == -1 || currentMovementInput.y == -1)
        { 
        isMovementPressed = true;
        }
        handleAnimation();

    }

    void OnEnable()
    {
       
    }
    void OnDisable()
    {
        
    }
    
    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + currentMovementInput * movementSpeed * Time.deltaTime); // move rigid body to new position
        // transform.LookAt(transform.position + rb.velocity);
        if (currentMovementInput.x > 0) {
            facing = Facing.Right;
            SetFacing();
        } else if (currentMovementInput.x < 0) {
            facing = Facing.Left;
            SetFacing();
        } else if (currentMovementInput.y > 0) {
            facing = Facing.Up;
            SetFacing();
        } else if (currentMovementInput.y < 0) {
            facing = Facing.Down;
            SetFacing();
        }
    }
     
    private void SetFacing()
    {
        /*
        if (facing == Facing.Right) { // right
            rb.rotation = 0;
        } else if (facing == Facing.Up) { // up
            rb.rotation = 90;
        } else if (facing == Facing.Left) { // left
            rb.rotation = 180;
        } else if (facing == Facing.Down) { // down
            rb.rotation = -90;
        }
        */
        
    }
    
}
