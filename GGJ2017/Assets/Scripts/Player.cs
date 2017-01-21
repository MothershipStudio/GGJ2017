using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private MagnetController magnetController;
    private ZTargeter ztargeter;

    public float KnockbackForce = 20f;
    public int MaxLife = 100, CurrentLife = 100;

	void Start () {
        magnetController = GetComponent<MagnetController>();
        ztargeter = GetComponent<ZTargeter>();
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.Q)) {
            if(ztargeter.currentTargetPositive != null)
                ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
            ztargeter.AimPositive();
            ztargeter.currentTargetPositive.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            if(ztargeter.currentTargetNegative != null)
                ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.clear);
            ztargeter.AimNegative();
            ztargeter.currentTargetNegative.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var op = collision.gameObject.GetComponent<ObjectProperties>();

        if (op != null) {
            if (op.Damages == ObjectProperties.DamageOn.PlayerOnly || op.Damages == ObjectProperties.DamageOn.Both)
            {
                this.CurrentLife -= (int)(op.Damage);
                if(this.CurrentLife <= 0)
                {
                    //TODO: funcao de morte
                }

                var knockback = (this.transform.position - collision.transform.position).normalized * KnockbackForce;
                GetComponent<Rigidbody2D>().AddForce(knockback);
            }
        }
    }
}
