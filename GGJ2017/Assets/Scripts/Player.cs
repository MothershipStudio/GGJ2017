using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private MagnetController magnetController;
    private ZTargeter ztargeter;

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
}
