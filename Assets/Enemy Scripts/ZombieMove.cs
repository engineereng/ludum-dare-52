using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove: MonoBehaviour

{
    public float minimum = -5.0f;
    public float maximum =  5.0f;
    public float moveSpeed = 1.0f;
    public float radius = 1.0f;

    static float t = 0.5f;
    private Collider2D collision;
    private float relativeMin;
    private float relativeMax;


    void Start()
    {
        relativeMax = transform.position.y + maximum;
        relativeMin = transform.position.y + minimum;
    }

    void FixedUpdate() 
    {
        collision = Physics2D.OverlapCircle(transform.position, radius);
        if (collision != null && collision.gameObject.GetComponent<MoveSheep>() != null) {
            Debug.Log(collision);
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, step);
            relativeMax = transform.position.y + maximum;
            relativeMin = transform.position.y + minimum;
            t = 0.5f;
        } else {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(relativeMin, relativeMax, t));
            t += 0.1f * Time.deltaTime;
            if (t > 1.0f)
            {
                float temp = relativeMax;
                relativeMax = relativeMin;
                relativeMin = temp;
                t = 0.0f;
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.DrawLine(new Vector3(transform.position.x, relativeMin, 0.0f), new Vector3(transform.position.x, relativeMax, 0.0f));
    }
}
