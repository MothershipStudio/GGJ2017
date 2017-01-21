using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrollingObject : MonoBehaviour
{

    public Transform[] targets;
    public int current { get; private set; }
    public int next { get; private set; }

    public float speed = 5f;
    public bool moving = false;

    private Rigidbody2D rb;

    private void Start()
    {
        current = 0;
        next = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartMoving()
    {
        moving = true;
    }

    public void StopMoving()
    {
        moving = false;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (moving)
        {
            if (next > targets.Length - 1)
                next = current - 1;
            else if (next < 0)
                next = current + 1;

            Vector2 movement = (targets[current].position - targets[next].position).normalized * speed + new Vector3(0, rb.velocity.y, 0);

            rb.velocity = movement;


            if (Vector2.Dot(targets[current].position - rb.transform.position, movement) < 0)
            {
                if (next < current)
                {
                    next--;
                    current--;
                }
                else
                {
                    next++;
                    current++;
                }
            }
        }
    }
}
