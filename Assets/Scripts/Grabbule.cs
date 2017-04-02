using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbule : MonoBehaviour {

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		transform.position += new Vector3(Random.value * 10 *  Time.deltaTime, 0, 0);
	}
}
