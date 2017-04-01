using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePowerUp : MonoBehaviour {

    public PowerUpType type;
    // Use this for initialization
    private bool hasTimedPowerUp;
	void Start () {
        type = PowerUpType.NONE;
        hasTimedPowerUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (type == PowerUpType.NONE)
            return;

        switch(type)
        {
            case PowerUpType.GIANT_BUBBLE :
                if (!hasTimedPowerUp)
                    StartCoroutine(PowerUp());
                    
                    gameObject.transform.localScale = new Vector3(2, 2, 2);
                
                break;

			case PowerUpType.SHOTBULLE:
		        if(Input.GetButton("Fire1"))
			        ShootABubble();
				type = PowerUpType.NONE;
		        break;

            case PowerUpType.NONE:
                gameObject.transform.localScale = Vector3.one;
                break;
        }
	}

	void ShootABubble()
	{
		gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0);
	}

    IEnumerator PowerUp()
    {
        hasTimedPowerUp = true;
        yield return new WaitForSeconds(10);

        type = PowerUpType.NONE;
        hasTimedPowerUp = false;
    }
}
