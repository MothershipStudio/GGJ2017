using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Transform plusHand;
    public Transform minusHand;
    private MagnetController magnetController;
    private ZTargeter ztargeter;

    private bool applyingPositive;
    private bool applyingNegative;
    private ArmsTrigger[] armsTrigger;
    private Rigidbody2D rb2d;

    void Start() {
        magnetController = GetComponent<MagnetController>();
        ztargeter = GetComponent<ZTargeter>();
        armsTrigger = GetComponentsInChildren<ArmsTrigger>();
        rb2d = GetComponent<Rigidbody2D>();

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
        if(Input.GetKeyDown(KeyCode.Q)) {
            if(ztargeter.currentTargetPositive != null)
                ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
            ztargeter.AimPositive();
            ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            if(ztargeter.currentTargetNegative != null)
                ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
            ztargeter.AimNegative();
            ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
        }

        if(Input.GetKeyDown(KeyCode.Z)) {
            SetArmsActivate(true);
            applyingPositive = true;
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            SetArmsActivate(true);
            applyingNegative = true;
        }

        if(Input.GetKeyUp(KeyCode.Z)) {
            SetArmsActivate(false);
            applyingPositive = false;
            magnetController.Release(ztargeter.currentTargetPositive);
        }

        if(Input.GetKeyUp(KeyCode.C)) {
            SetArmsActivate(false);
            applyingNegative = false;
            magnetController.Release(ztargeter.currentTargetNegative);
        }
    }

    private void FixedUpdate() {
        if(applyingPositive) {
            if((ztargeter.currentTargetPositive.position - (Vector2)plusHand.position).sqrMagnitude > 2 * magnetController.magnetMaxRange)
                ztargeter.AimPositive();
            magnetController.AplyMagnetForce(plusHand.position, ztargeter.currentTargetPositive, 1, 1);
        }

        if(applyingNegative) {
            if((ztargeter.currentTargetNegative.position - (Vector2)plusHand.position).sqrMagnitude > 2 * magnetController.magnetMaxRange)
                ztargeter.AimNegative();
            magnetController.AplyMagnetForce(minusHand.position, ztargeter.currentTargetPositive, -1, 1);
        }

    }

}
