using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour {

    public PowerUpType type;
    
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        ArrayList players = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>().GetPlayers();
        foreach (GameObject p in players)
        {
            float dis = MathUtils.XZDist(p.transform.position, gameObject.transform.position);

            if (dis < 1)
            {
                PowerUpComponent bpu = p.GetComponent<PowerUpComponent>();
                if (bpu.type == PowerUpType.NONE)
                {
                    bpu.type = this.type;
                    GameObject.Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
