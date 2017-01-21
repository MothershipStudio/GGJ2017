using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : MonoBehaviour {
    
    public float PressureNeeded = 0f;
    public UnityEvent OnPressurePlateActive;
    public UnityEvent OnPressurePlateDeactive;

    //Read only, used to know if the pressure plate is active, for animation purposes or others
    public bool isActived { get; private set; }

    private float CurrentPressure = 0f;

    public void OnCollisionStay2D(Collision2D collision)
    {
        CurrentPressure += collision.gameObject.GetComponent<Rigidbody2D>().mass;

        if(CurrentPressure >= PressureNeeded)
        {
            OnPressurePlateActive.Invoke();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        CurrentPressure -= collision.gameObject.GetComponent<Rigidbody2D>().mass;

        if (CurrentPressure < PressureNeeded)
        {
            OnPressurePlateDeactive.Invoke();
        }
    }

}
