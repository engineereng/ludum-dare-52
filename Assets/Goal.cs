using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.TryGetComponent<HasScore>(out var otherHasScore))
        {
            Debug.Log("Scored: " + otherHasScore.score + "points!");
            Destroy(other.gameObject); // destroy the moving sheep
            // TODO maybe spawn a grazing sheep?
        }
    }
}
