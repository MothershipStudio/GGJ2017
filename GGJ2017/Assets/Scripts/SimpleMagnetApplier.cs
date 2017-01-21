using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMagnetApplier : MonoBehaviour {
    MagnetController mc;

    private void Start() {
        mc = GetComponent<MagnetController>();
    }

    private void Update() {
        if(Input.GetMouseButton(0)) {
            var wmpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(wmpos, Vector2.zero);
            if(hit.collider != null) {
                //mc.targetRb2d = hit.collider.GetComponent<Rigidbody2D>();
                //mc.Attract();
            }
        }

        if(Input.GetMouseButton(1)) {
            var wmpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(wmpos, Vector2.zero);
            if(hit.collider != null) {
                //mc.targetRb2d = hit.collider.GetComponent<Rigidbody2D>();
                //mc.Repel();
            }
        }

        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
            //mc.Release();
        }
    }
}
