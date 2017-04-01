using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePowerUp : MonoBehaviour {

    public PowerUpType type;
	// Use this for initialization
	void Start () {
        type = PowerUpType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
        if (type == PowerUpType.NONE)
            return;

        switch(type)
        {
            case PowerUpType.GIANT_BUBBLE :  
                
                break;
        }
	}
}
