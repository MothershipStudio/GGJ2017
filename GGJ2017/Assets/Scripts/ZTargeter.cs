using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTargeter : MonoBehaviour {
    public LayerMask targetMask;
    public Rigidbody2D currentTarget { get; set; }
    SortedList<float, Rigidbody2D> targets = new SortedList<float, Rigidbody2D>();

    Vector2 w_screen_size;
    int currentTargetIndex = 0;

    private void Start() {
        w_screen_size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            UpdateTargets();
            foreach(var target in targets) {
                Debug.Log(target.Value.name);
            }
        }
    }

    public void Aim() {
        currentTarget = targets.Values[currentTargetIndex];
        currentTargetIndex = (currentTargetIndex + 1) % targets.Count;

    }

    void Unaim() {
        currentTargetIndex = 0;
    }

    void UpdateTargets() {
        targets.Clear();
        var hits = Physics2D.BoxCastAll(Camera.main.transform.position, w_screen_size * 100, 0, Vector2.zero, 0, targetMask);

        foreach(var hit in hits) {
            var tpos = hit.transform.position;
            Vector3 toTarget = tpos - transform.position;
            int dirMask = (int)(-Mathf.Sign(transform.lossyScale.x * toTarget.x) + 1) << 16;
            targets.Add(Vector3.SqrMagnitude(toTarget) + dirMask, hit.collider.GetComponent<Rigidbody2D>());
        }
    }
}
