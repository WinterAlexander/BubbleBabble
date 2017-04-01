using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public PowerUpType type;
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ArrayList players = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>().GetPlayers();
        foreach(GameObject p in players)
        {
            float dis = XZDist(p.transform.position, gameObject.transform.position);
            SphereCollider collider = p.GetComponent<SphereCollider>();

            Debug.Log(dis);
            if (dis < 1)
            {
                p.GetComponent<BubblePowerUp>().type = this.type;
                GameObject.Destroy(gameObject);
            }
        }
    }

    private float XZDist(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(
            (a.x - b.x) * (a.x - b.x) +
            (a.z - b.z) * (a.z - b.z)
            );
    }
}
