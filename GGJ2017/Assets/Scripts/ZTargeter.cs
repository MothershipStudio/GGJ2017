using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTargeter : MonoBehaviour {
    public LayerMask targetMask;
    public Rigidbody2D currentTargetPositive { get; private set; }
    public Rigidbody2D currentTargetNegative { get; private set; }
    
    SortedList<float, Rigidbody2D> targets = new SortedList<float, Rigidbody2D>();

    Vector2 w_screen_size;
    int currentTargetIndexPositive = 0;
    int currentTargetIndexNegative = 0;

    private void Start() {
        w_screen_size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            UpdateTargets();
            Debug.Log("Updated targets");
            foreach(var target in targets) {
                Debug.Log(target.Value.name);
            }
        }
    }

    public void AimPositive() {
        UpdateTargets();
        currentTargetPositive = targets.Values[currentTargetIndexPositive];
        currentTargetIndexPositive = (currentTargetIndexPositive + 1) % targets.Count;
    }

    public void AimNegative() {
        UpdateTargets();
        currentTargetNegative = targets.Values[currentTargetIndexNegative];
        currentTargetIndexNegative = (currentTargetIndexNegative + 1) % targets.Count;
    }

    void UpdateTargets() {
        targets.Clear();
        var hits = Physics2D.BoxCastAll(Camera.main.transform.position, w_screen_size * 100, 0, Vector2.zero, 0, targetMask);
        Debug.Log(hits.Length);

        foreach(var hit in hits) {
            var tpos = hit.transform.position;
            Vector3 toTarget = tpos - transform.position;
            int dirMask = (int)(-Mathf.Sign(transform.lossyScale.x * toTarget.x) + 1) << 16;
            targets.Add(Vector3.SqrMagnitude(toTarget) + dirMask, hit.collider.GetComponent<Rigidbody2D>());
        }
    }
}
