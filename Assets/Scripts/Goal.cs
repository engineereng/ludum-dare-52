using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent onGoalTriggered;

    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.TryGetComponent<HasScore>(out var otherHasScore))
        {
            Debug.Log("Scored: " + otherHasScore.score + "points!");
            onGoalTriggered.Invoke();
            Destroy(other.gameObject); // destroy the moving sheep
            // TODO maybe spawn a grazing sheep?
        }
    }
}
