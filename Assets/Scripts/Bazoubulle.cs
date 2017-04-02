using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazoubulle : MonoBehaviour
{
	public GameObject holder;
	public Vector3 baseVel;

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		GetComponent<Rigidbody>().velocity = baseVel;
		print(GetComponent<Rigidbody>().velocity);
	}

	void OnCollisionEnter()
	{
		//Destroy(gameObject);
	}
}
