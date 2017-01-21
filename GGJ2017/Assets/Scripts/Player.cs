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
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Z)) {
            ztargeter.ai
        }
	}
}
