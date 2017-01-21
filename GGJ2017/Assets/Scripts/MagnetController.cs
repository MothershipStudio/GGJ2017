using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour {
    public float magnetMaxRange = 30f;
    public float magnetMinRange = .01f;
    public float magnectForce = 10f;

    private Rigidbody2D rb2d;
    float origivalGravityScale;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        origivalGravityScale = origivalGravityScale = rb2d.gravityScale;
    }

    public void AplyMagnetForce(Vector2 origin, Rigidbody2D targetRb2d, int polarity, int targetPolarity) {
        Vector2 magnetForceDir = targetRb2d.position - origin;
        magnetForceDir *= polarity * targetPolarity;
        AplyMagnetForce(targetRb2d, magnetForceDir);
    }

    void AplyMagnetForce(Rigidbody2D targetRb2d, Vector2 magnetForceDir) {
        if(magnetForceDir.sqrMagnitude < magnetMaxRange) {
            magnetForceDir = magnetForceDir.normalized * magnectForce;
            //TODO: aplicar obj sem atrito
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
}
