using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrollingObject : MonoBehaviour
{

    public Transform[] targets;
    [HideInInspector]
    public int current = 0;
    [HideInInspector]
    public int next = 1;

    public float speed = 5f;
    private bool moving = false;

    public UnityEvent<Collision2D, PatrollingObject> OnObjectOnTrack;

    private Rigidbody2D rb;

    public virtual void StartPatrol()
    {
        current = 0;
        next = 1;
        rb = GetComponent<Rigidbody2D>();
        this.moving = true;

        OnObjectOnTrack = new CollisionEvent();
    }


    public virtual void StartMoving()
    {
        moving = true;
    }

    public virtual void StopMoving()
    {
        moving = false;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    protected void FixedUpdate()
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
                UpdateNext();
            }
        }
    }

    private void UpdateNext()
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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnObjectOnTrack != null)
            OnObjectOnTrack.Invoke(collision, this);
    }
}

public class CollisionEvent : UnityEvent<Collision2D, PatrollingObject>
{

}
