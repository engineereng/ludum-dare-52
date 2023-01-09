using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : Detector
{
    [SerializeField]
    private float playerDetectionRange = 5;
    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;
    [SerializeField]
    private bool showGizmos = true;
    private List<Transform> colliders;

    public override void Detect(AIData aiData)
    {
        // Check if player is near
        Debug.Log("Detecting player");
        var playerCollider = Physics2D.OverlapCircle(transform.position, playerDetectionRange, playerLayerMask);
        if (playerCollider != null)
        {
            //Check if the player can be seen by detector
            var direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerDetectionRange, obstaclesLayerMask);

            //Make sure that the collider is on the "Player" layer
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * playerDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            //Enemy cannot be seen by detector
            colliders = null;
        }
        aiData.targets = colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
        if (colliders == null)
            return;
        
        
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
