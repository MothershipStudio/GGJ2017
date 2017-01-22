using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectProperties : MonoBehaviour
{
    public enum Polarity
    {
        Neutral = 0,
        Positive = 1,
        Negative = -1
    }

    public enum DamageOn
    {
        PlayerOnly,
        EnemyOnly,
        Both
    }

    public DamageOn Damages;
    public float Damage = 0f;
    public Polarity ObjectPolarity;
}