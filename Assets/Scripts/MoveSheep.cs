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

    public bool isWandering = false;

    [SerializeField]
    public List<SteeringBehaviour> steeringBehaviours;
    [SerializeField]
    private AIData aiData;
    Vector2 resultDirection = Vector2.zero;

    // Update is called once per frame

    void FixedUpdate()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
            rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime); // move rigid body to new position
        } else if (isWandering)
        {
            rb.MovePosition(rb.position + WanderAround() * movementSpeed * Time.deltaTime);
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
                // soul can now randomly move around when player character isn't interacting with it
                isWandering = true;
                Vector2 direction = transform.position - other.transform.position;
                movement = direction / direction.magnitude / direction.magnitude * fleeDistance;
                moveTimer = moveDuration;
            } else if (bark.BarkState == Bark.Barks.Stop) {
                moveTimer = 0;
            }
        }
    }

    public Vector2 WanderAround()
    {
        
        float[] danger = new float[8];
        float[] interest = new float[8];

        foreach (SteeringBehaviour behaviour in steeringBehaviours)
        {
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
        }

        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            outputDirection += Directions.eightDirections[i] * danger[i];
        }
        outputDirection.Normalize();
        resultDirection = -outputDirection;
        Debug.DrawRay(rb.position, outputDirection, Color.magenta);
        return resultDirection;
    }
}
