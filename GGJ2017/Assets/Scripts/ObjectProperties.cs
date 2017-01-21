using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectProperties : MonoBehaviour
{
    public enum Polarity
    {
        Neutral = 0,
        Positive = 1,
        Negative = -1
    }

    public float Damage = 0f;
    public Polarity ObjectPolarity;

    public Vector2 Knockback;
    
}