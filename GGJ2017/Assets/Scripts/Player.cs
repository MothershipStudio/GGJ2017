using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Transform hand;
    private MagnetController magnetController;
    private ZTargeter ztargeter;

    private bool applyingPositive;
    private bool applyingNegative;
    private ArmsTrigger[] armsTrigger;
    private Rigidbody2D rb2d;

    private bool isAlive = true;

    public float KnockbackForce = 20f;
    public int MaxLife = 100, CurrentLife = 100;
    public GameObject redWave;
    public GameObject blueWave;
    Animator animator;

    void Start() {
        magnetController = GetComponent<MagnetController>();
        ztargeter = GetComponent<ZTargeter>();
        armsTrigger = GetComponentsInChildren<ArmsTrigger>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        for(int i = 0; i < armsTrigger.Length; i++) {
            armsTrigger[i].onTriggerEnter.AddListener(OnArmsTriggerEnter);
            armsTrigger[i].GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void SetArmsActivate(bool setActive) {
        for(int i = 0; i < armsTrigger.Length; i++) {
            armsTrigger[i].GetComponent<CircleCollider2D>().enabled = setActive;
        }
    }

    void OnArmsTriggerEnter(Collider2D col, Transform dad) {
        if(applyingPositive && ztargeter.currentTargetPositive.gameObject == col.gameObject && rb2d.mass > ztargeter.currentTargetPositive.mass) {
            Debug.Log("OnArmsTriggerEnter: " + col.gameObject.name);
            ztargeter.currentTargetPositive.velocity = Vector2.zero;
            applyingPositive = false;
            ztargeter.currentTargetPositive.gravityScale = 0;
            col.transform.parent = (dad);
        }
    }

    void Aim(Rigidbody2D oldTar, Rigidbody2D newTar) {
        if(ztargeter.currentTargetPositive != null)
            ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
        ztargeter.AimPositive();
        ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
    }

    void Update() {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (ztargeter.currentTargetPositive != null)
                    ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
                ztargeter.AimPositive();
                ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ztargeter.currentTargetNegative != null)
                    ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
                ztargeter.AimNegative();
                ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                SetArmsActivate(true);
                applyingPositive = true;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SetArmsActivate(true);
                applyingNegative = true;
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                SetArmsActivate(false);
                applyingPositive = false;
                magnetController.Release(ztargeter.currentTargetPositive);
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                SetArmsActivate(false);
                applyingNegative = false;
                magnetController.Release(ztargeter.currentTargetNegative);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var op = collision.gameObject.GetComponent<ObjectProperties>();

        if (op != null)
        {
            if (op.Damages == ObjectProperties.DamageOn.PlayerOnly || op.Damages == ObjectProperties.DamageOn.Both)
            {
                this.CurrentLife -= (int)(op.Damage);
                if (this.CurrentLife <= 0)
                {
                    isAlive = false;
                    KillIt();
                }

                var knockback = (this.transform.position - collision.transform.position).normalized * KnockbackForce;
                GetComponent<Rigidbody2D>().AddForce(knockback);
            }
        }
    }

    public void KillIt()
    {

    } 

    private void FixedUpdate() {
        if (isAlive)
        {
            if (applyingPositive)
            {
                var hand2target = ztargeter.currentTargetPositive.position - (Vector2)hand.position;
                if ((hand2target).sqrMagnitude > 2 * magnetController.magnetMaxRange)
                    ztargeter.AimPositive();
                magnetController.AplyMagnetForce(hand.position, ztargeter.currentTargetPositive, 1, 1);
                redWave.transform.rotation = Quaternion.Euler(180 - Mathf.Acos(Vector2.Dot(hand2target.normalized, Vector2.right)) * Mathf.Rad2Deg, 90, 90);
            }

            if (applyingNegative)
            {
                var hand2target = ztargeter.currentTargetNegative.position - (Vector2)hand.position;
                if ((hand2target).sqrMagnitude > 2 * magnetController.magnetMaxRange)
                    ztargeter.AimNegative();
                magnetController.AplyMagnetForce(hand.position, ztargeter.currentTargetPositive, -1, 1);
                blueWave.transform.rotation = Quaternion.Euler(180 - Mathf.Acos( Vector2.Dot(hand2target.normalized, Vector2.right)) * Mathf.Rad2Deg, 90, 90);
            }

            animator.SetBool("plusFiring", applyingPositive || applyingNegative);
            redWave.SetActive(applyingPositive);
            blueWave.SetActive(applyingNegative);
        }
    }

}
