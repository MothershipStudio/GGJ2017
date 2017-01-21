using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour {
    public float magnetMaxRange = 5f;
    public float magnetMinRange = .01f;
    public float magnectForce = 10f;

    private Rigidbody2D rb2d;
    public Rigidbody2D targetRb2d { get; set; }
    
    
    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    //TODO: animação mesmo se for fora do range
    public void Attract() {
        Vector2 magnetForceDir = targetRb2d.position - (Vector2)transform.position;
        AplyMagnetForce(targetRb2d, magnetForceDir);
    }

    public void Repel() {
        Vector2 magnetForceDir = (Vector2)transform.position - targetRb2d.position;
        AplyMagnetForce(targetRb2d, magnetForceDir);
    }

    public void AplyMagnetForce(Rigidbody2D targetRb2d, int polarity, int targetPolarity) {
        Vector2 magnetForceDir = targetRb2d.position - (Vector2)transform.position;
        magnetForceDir *= polarity * targetPolarity;
        AplyMagnetForce(targetRb2d, magnetForceDir);
    }

    void AplyMagnetForce(Rigidbody2D targetRb2d, Vector2 magnetForceDir) {
        if(magnetForceDir.sqrMagnitude < magnetMaxRange) {
            magnetForceDir = magnetForceDir.normalized * magnectForce;
            //TODO: aplicar obj sem atrito
            if(rb2d.mass < targetRb2d.mass) {
                rb2d.gravityScale = 0;
                rb2d.velocity = magnetForceDir;

            } else {
                targetRb2d.gravityScale = 0;
                targetRb2d.velocity = -magnetForceDir;
            }
        }
    }

    public void Release() {
        rb2d.gravityScale = 1;
        targetRb2d.gravityScale = 1;
    }
}
