using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject player = GameObject.Find("Bulle Player");
        Vector3 playerV = player.transform.position;
        gameObject.transform.position = new Vector3(playerV.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}
}
