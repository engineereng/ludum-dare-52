using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSheep : MonoBehaviour
{

    public float movementSpeed = 1f;
    public float fleeDistance = 0.2f;
    public Rigidbody2D rb;
    Vector2 movement;
    public float moveDuration;

    public float moveTimer = 0f;
    // Update is called once per frame

    void FixedUpdate()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
            rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime); // move rigid body to new position
        } else {
            rb.velocity = Vector2.zero; // stop moving
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("Hit something!");
        // if (other.CompareTag("Player"))
        Bark bark = other.gameObject.GetComponentInParent<Bark>();
        if (bark != null && bark.IsBarking)
        {
            // Debug.Log("Hit player!");
            // if the other object is the player character
            if (bark.BarkState == Bark.Barks.Go) {
                Vector2 direction = transform.position - other.transform.position;
                movement = direction / direction.magnitude / direction.magnitude * fleeDistance;
                moveTimer = moveDuration;
            } else if (bark.BarkState == Bark.Barks.Stop) {
                moveTimer = 0;
            }
        }
    }
}
