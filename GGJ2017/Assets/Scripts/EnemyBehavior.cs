using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehavior : PatrollingObject {
    private void Start()
    {
        base.StartPatrol();
        OnObjectOnTrack.AddListener(PatrolBehavior.ApplyDamage);
        OnObjectOnTrack.AddListener(PatrolBehavior.ChangeDirection);
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
