using UnityEngine;
using UnityEngine.Events;

class ArmsTriggerEvent : UnityEvent<Collider2D, Transform> {}

[RequireComponent(typeof(CircleCollider2D))]
public class ArmsTrigger : MonoBehaviour {
    public UnityEvent<Collider2D, Transform> onTriggerEnter = new ArmsTriggerEvent();

    private void OnTriggerEnter2D(Collider2D collision) {
        onTriggerEnter.Invoke(collision, transform);
    }
}
