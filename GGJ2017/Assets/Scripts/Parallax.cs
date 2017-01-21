using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public float speed = .01f;

    Renderer r;
    Transform cam;
    Vector2 start_pos;

    private void Start() {
        r = GetComponent<MeshRenderer>();
        cam = Camera.main.transform;
        start_pos = cam.transform.position;
    }

    private void FixedUpdate() {
        r.material.SetTextureOffset("_MainTex", Vector2.right * (cam.position.x - start_pos.x) * speed + Vector2.up * (cam.position.y - start_pos.y) * speed);
        
    }
}
