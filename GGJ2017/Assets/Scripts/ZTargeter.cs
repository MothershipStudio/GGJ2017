using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZTargeter : MonoBehaviour {
    public LayerMask targetMask;

    private Rigidbody2D _currentTargetPositive;
    private Rigidbody2D _currentTargetNegative;
    public Rigidbody2D currentTargetPositive {
        get {
            AimPositive();
            return _currentTargetPositive;
        }
        private set {
            _currentTargetPositive = value;
        }
    }
    public Rigidbody2D currentTargetNegative {
        get {
            AimNegative();
            return _currentTargetNegative;
        }
        private set {
            _currentTargetNegative = value;
        }
    }

    SortedList<float, Rigidbody2D> targets = new SortedList<float, Rigidbody2D>();

    Vector2 w_screen_size;
    int currentTargetIndexPositive = 0;
    int currentTargetIndexNegative = 0;

    private void Start() {
        w_screen_size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z)) * 2;
    }

    public void AimPositive() {
        UpdateTargets();
        if(targets.Count == 0)
            return;
        currentTargetPositive = targets.Values[currentTargetIndexPositive % targets.Count];
        currentTargetIndexPositive = (currentTargetIndexPositive + 1) % targets.Count;
    }

    public void AimNegative() {
        UpdateTargets();
        if(targets.Count == 0)
            return;
        currentTargetNegative = targets.Values[currentTargetIndexNegative % targets.Count];
        currentTargetIndexNegative = (currentTargetIndexNegative + 1) % targets.Count;
    }

    public void Reset() {
        _currentTargetNegative = _currentTargetPositive = null;
        currentTargetIndexNegative = currentTargetIndexPositive = 0;
    }

    void UpdateTargets() {
        targets.Clear();
        var hits = Physics2D.BoxCastAll(Camera.main.transform.position, w_screen_size, 0, Vector2.zero, 0, targetMask);
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        foreach(var hit in hits) {
            var tpos = hit.transform.position;
            Vector3 toTarget = tpos - mousePos;
            targets.Add(toTarget.sqrMagnitude, hit.collider.GetComponent<Rigidbody2D>());
        }
    }
}
