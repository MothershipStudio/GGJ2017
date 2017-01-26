using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour {
    public UnityEvent onKill;
    private MagnetController magnetController;
    private ZTargeter ztargeter;
    private Rigidbody2D rb2d;

    private bool isAlive = true;

    public float KnockbackForce = 5000f;
    public int MaxLife = 100, CurrentLife = 100;
    Animator animator;

    void Start() {
        magnetController = GetComponent<MagnetController>();
        ztargeter = GetComponent<ZTargeter>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnArmsTriggerEnter(Collider2D col, Transform dad) {
        //if(applyingPositive && ztargeter.currentTargetPositive.gameObject == col.gameObject && rb2d.mass > ztargeter.currentTargetPositive.mass) {
        //    Debug.Log("OnArmsTriggerEnter: " + col.gameObject.name);
        //    ztargeter.currentTargetPositive.velocity = Vector2.zero;
        //    applyingPositive = false;
        //    ztargeter.currentTargetPositive.gravityScale = 0;
        //    col.transform.parent = (dad);
        //}
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (ztargeter.currentTargetPositive != null && ztargeter.currentTargetPositive.GetComponent<Renderer>())
                    ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
                ztargeter.AimPositive();
                if(ztargeter.currentTargetPositive.GetComponent<Renderer>())
                    ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ztargeter.currentTargetNegative != null && ztargeter.currentTargetNegative.GetComponent<Renderer>())
                    ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
                ztargeter.AimNegative();
                if(ztargeter.currentTargetPositive.GetComponent<Renderer>())
                    ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
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
                GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
            }
        }
    }

    public void KillIt()
    {
        onKill.Invoke();
    } 

    

}
