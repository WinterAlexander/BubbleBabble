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
                gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;

			case PowerUpType.SHOTBULLE:
		        if(Input.GetButton("Fire1"))
			        ShootABubble();
				type = PowerUpType.NONE;
		        break;

        }
	}

	void ShootABubble()
	{
		gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0);
	}
}
