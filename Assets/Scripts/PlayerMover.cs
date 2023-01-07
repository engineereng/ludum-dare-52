using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public enum Facing {Right, Up, Left, Down};
    public Facing facing; // 0 = right, 1 = up, 2 = left, 3 = down

    public float movementSpeed = .5f;

    public Rigidbody2D rb;
    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // L / R movement; [-1, 1]
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime); // move rigid body to new position
        // transform.LookAt(transform.position + rb.velocity);
        if (movement.x > 0) {
            facing = Facing.Right;
            SetFacing();
        } else if (movement.x < 0) {
            facing = Facing.Left;
            SetFacing();
        } else if (movement.y > 0) {
            facing = Facing.Up;
            SetFacing();
        } else if (movement.y < 0) {
            facing = Facing.Down;
            SetFacing();
        }
    }

    private void SetFacing()
    {
        if (facing == Facing.Right) { // right
            rb.rotation = 0;
        } else if (facing == Facing.Up) { // up
            rb.rotation = 90;
        } else if (facing == Facing.Left) { // left
            rb.rotation = 180;
        } else if (facing == Facing.Down) { // down
            rb.rotation = -90;
        }
    }
}
