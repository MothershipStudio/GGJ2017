using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Switch : MonoBehaviour
{
    public bool mustMantainHold;
    public UnityEvent OnSwitchDesactive;
    public UnityEvent OnSwitchActive;

    //use mainly for animation purposes
    public bool isActive { get; private set; }

    private bool canActivate = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            canActivate = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canActivate = false;
            isActive = false;
        }
    }

    public void Activate()
    {
        if (canActivate)
        {
            isActive = true;
            OnSwitchActive.Invoke();
        }
    }

    public void ReleaseHold()
    {
        if (mustMantainHold)
        {
            isActive = false;
            OnSwitchDesactive.Invoke();
        }
    }
    
    public void Deactivate()
    {
        if (canActivate)
        {
            isActive = false;
            OnSwitchActive.Invoke();
        }
    }
}
