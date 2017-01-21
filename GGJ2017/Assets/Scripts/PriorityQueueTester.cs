using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueueTester : MonoBehaviour {

    // Use this for initialization
    void Start() {
        PriorityQueue<Vector3> pq = new PriorityQueue<Vector3>();
        pq.Enqueue(Vector3.zero, 1);
        pq.Enqueue(Vector3.one, 1);
        pq.Enqueue(Vector3.zero, 1);
    }
}
