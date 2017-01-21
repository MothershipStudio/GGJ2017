using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTargeter : MonoBehaviour {
    public LayerMask targetMask;
    Vector3 currentTarget;
    SortedList<Vector3, float> targets = new SortedList<Vector3, float>();

    Vector2 w_screen_size;

        

    private void Start() {
        w_screen_size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void UpdateTargets() {
        targets.Clear();
        var hits = Physics2D.BoxCastAll(Camera.main.transform.position, w_screen_size, 0, Vector2.zero, targetMask);

        foreach(var hit in hits) {
            var tpos = hit.transform.position;
            targets.Add(tpos, Vector3.SqrMagnitude(tpos - transform.position));
        }
    }
}
