using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour {
    public float magnetMaxRange = 30f;
    public float magnetMinRange = .01f;
    public float magnectForce = 10f;
    public LayerMask targetsMask;
    private bool applyingPositive;
    private bool applyingNegative;
    private Animator animator;

    private Rigidbody2D rb2d;
    float origivalGravityScale;
    public GameObject redWave;
    public GameObject blueWave;
    public Transform hand;

    private Rigidbody2D positiveRb;
    private Rigidbody2D negativeRb;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        origivalGravityScale = origivalGravityScale = rb2d.gravityScale;
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            applyingPositive = true;
            positiveRb = GetNearestRb2d();
        }

        if(Input.GetMouseButtonDown(1)) {
            applyingNegative = true;
            negativeRb = GetNearestRb2d();
        }

        if(Input.GetMouseButtonUp(0)) {
            applyingPositive = false;
            //Release(ztargeter.currentTargetPositive);
        }

        if(Input.GetMouseButtonUp(1)) {
            applyingNegative = false;
            //magnetController.Release(ztargeter.currentTargetNegative);
        }
    }

    Rigidbody2D GetNearestRb2d() {
        var mouse_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        var hit = Physics2D.CircleCast(mouse_pos, 5f, Vector2.zero, 0, targetsMask);
        if(hit.collider != null) {
            Debug.Log(hit.collider.name);
            return hit.collider.GetComponent<Rigidbody2D>();
        }

        return null;
    }

    public void AplyMagnetForce(Vector2 origin, Rigidbody2D targetRb2d, int polarity, int targetPolarity) {
        Vector2 magnetForceDir = targetRb2d.position - origin;
        magnetForceDir *= polarity * targetPolarity;
        AplyMagnetForce(targetRb2d, magnetForceDir);
    }

    void AplyMagnetForce(Rigidbody2D targetRb2d, Vector2 magnetForceDir) {
        if(magnetForceDir.sqrMagnitude < magnetMaxRange) {
            magnetForceDir = magnetForceDir.normalized * magnectForce;
            if(rb2d.mass < targetRb2d.mass) {
                rb2d.gravityScale = 0;
                rb2d.AddForce(magnetForceDir);

            } else {
                targetRb2d.gravityScale = 0;
                targetRb2d.AddForce(-magnetForceDir);
            }
        }
    }

    public void Release(Rigidbody2D targetRb2d) {
        rb2d.gravityScale = origivalGravityScale;
        if(targetRb2d != null) {
            targetRb2d.simulated = true;
            targetRb2d.transform.parent = (null);
            targetRb2d.gravityScale = 1;
        }
            
    }

    private void FixedUpdate() {
        if(applyingPositive && positiveRb) {
            var hand2target = positiveRb.position - (Vector2)hand.position;
            AplyMagnetForce(hand2target, positiveRb, 1, 1);
            redWave.transform.rotation = Quaternion.Euler(180 - Mathf.Acos(Vector2.Dot(hand2target.normalized, Vector2.right)) * Mathf.Rad2Deg, 90, 90);
            Debug.Log("applyingPositive " +positiveRb.name);
            //var hand2target = ztargeter.currentTargetPositive.position - (Vector2)hand.position;
            //if((hand2target).sqrMagnitude > 2 * magnetController.magnetMaxRange)
            //    ztargeter.AimPositive();
            //AplyMagnetForce(hand.position, ztargeter.currentTargetPositive, 1, 1);
        }

        if(applyingNegative && negativeRb) {
            Debug.Log("applyingNegative " + negativeRb.name);
            var hand2target = negativeRb.position - (Vector2)hand.position;
            AplyMagnetForce(hand2target, negativeRb, -1, 1);
            blueWave.transform.rotation = Quaternion.Euler(180 - Mathf.Acos(Vector2.Dot(hand2target.normalized, Vector2.right)) * Mathf.Rad2Deg, 90, 90);
            //var hand2target = ztargeter.currentTargetNegative.position - (Vector2)hand.position;
            //if((hand2target).sqrMagnitude > 2 * magnetMaxRange)
            //    ztargeter.AimNegative();
            //magnetController.AplyMagnetForce(hand.position, ztargeter.currentTargetPositive, -1, 1);
            //blueWave.transform.rotation = Quaternion.Euler(180 - Mathf.Acos(Vector2.Dot(hand2target.normalized, Vector2.right)) * Mathf.Rad2Deg, 90, 90);
        }

        animator.SetBool("plusFiring", applyingPositive || applyingNegative);
        redWave.SetActive(applyingPositive);
        blueWave.SetActive(applyingNegative);
    }
}
