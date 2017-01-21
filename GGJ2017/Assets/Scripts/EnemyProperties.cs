using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public int maxLife = 100;
    public float currentLife = 100f;
    
    public float regenTime = 5f;
    public float initialTime = 0f;

    public float regen = 1f;
    
	
	// Update is called once per frame
	void Update () {
		if(currentLife < maxLife)
        {
            if (initialTime == 0)
                initialTime = Time.time;
            if(regenTime < (Time.time - initialTime))
            {
                currentLife += regen * Time.deltaTime;
            }
        }
        if(currentLife > maxLife)
        {
            currentLife = 100f;
            initialTime = 0;
        }
	}
}
