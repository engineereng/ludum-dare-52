using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent onGoalTriggered;
    [SerializeField] private float scoreNeeded = 1f;
    [SerializeField] private float score = 0f;

    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.TryGetComponent<HasScore>(out var otherHasScore))
        {
            float newScore = otherHasScore.score;
            Debug.Log("Scored: " + newScore + "points!");
            score += newScore;
            Destroy(other.gameObject); // destroy the moving sheep
            if (score >= scoreNeeded) {
                onGoalTriggered.Invoke();
            }
        }
    }
}
