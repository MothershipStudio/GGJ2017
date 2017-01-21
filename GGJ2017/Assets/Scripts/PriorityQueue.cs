
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueueNode<T> {
    public PriorityQueueNode() {
        tuple = new Tupple();
    }

    public PriorityQueueNode(T coord, float order) {
        this.tuple = new Tupple();
        this.tuple.coord = coord;
        this.tuple.order = order;
    }

    public class Tupple {
        public T coord;
        public float order;
    }

    public Tupple tuple;
    public PriorityQueueNode<T> next = null;

}

public class PriorityQueue <T> : IEnumerable<T> {
    public int Count { get; private set; }
    public bool Empty { get { return Count == 0; } }

    private PriorityQueueNode<T> head = null;

    public void Enqueue(T val, float order) {
        var item = new PriorityQueueNode<T>(val, order);

        if(head == null) {
            head = item;
        } else if(head.tuple.order >= order) {
            item.next = head;

            head = item;
        } else {
            PriorityQueueNode<T> it = head;
            PriorityQueueNode<T> previous_it = it;
            while(it != null && it.tuple.order < order) {
                previous_it = it;
                it = it.next;
            }
            previous_it.next = item;
            item.next = it;
        }

        Count++;
    }

    public T Dequeue() {
        if(head == null)
            return default(T);

        var val = head.tuple.coord;
        head = head.next;
        Count--;
        return val;
    }

    public T Peek() {
        if(head == null)
            return default(T);

        return head.tuple.coord;
    }

    public IEnumerator<T> GetEnumerator() {
        PriorityQueueNode<T> it = head;

        while(it != null) {
            yield return it.tuple.coord;
            it = it.next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        PriorityQueueNode<T> it = head;

        while(it != null) {
            yield return it.tuple.coord;
            it = it.next;
        }
    }

    public T this[int i] {
        get {
            PriorityQueueNode<T> it = head;
            while(i-- > 0) {
                it = it.next;
            }
            return it.tuple.coord;
        }
    }

}