using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazoubulle : MonoBehaviour
{
	public GameObject holder;
	public Vector3 baseVel;

	public float targetingIntensity = 0.5f;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = baseVel;
        //Shake.ShakeEffect(Camera.main.gameObject, 0.05f, 0.05f);
	}

	// Update is called once per frame
	void Update()
	{
		GameObject closest = null;
		Vector3 vecToClosest = new Vector3(0, 5, 0);

		foreach(GameObject player in GetPlayers())
		{
			if(player == holder)
				continue;

			Vector3 thisDiff = player.transform.position - transform.position;

			if(thisDiff.magnitude < vecToClosest.magnitude && Vector3.Angle(thisDiff, GetComponent<Rigidbody>().velocity) < 90)
			{
				closest = player;
				vecToClosest = thisDiff;
			}
		}
		
		Vector3 direction;

		if(closest == null)
		{
			direction = GetComponent<Rigidbody>().velocity.normalized;
		}
		else
		{
			float value = (float)Math.Pow(1f - targetingIntensity, Time.deltaTime);

			direction = GetComponent<Rigidbody>().velocity.normalized * value;
			direction += vecToClosest.normalized * (1f - value);
		}


		GetComponent<Rigidbody>().velocity = direction * baseVel.magnitude;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			foreach(GameObject player in GetPlayers())
				if(collision.gameObject != player)
					player.GetComponent<Rigidbody>().AddExplosionForce(1000f, collision.contacts[0].point, 3f, 0);

			Destroy(Instantiate(holder.GetComponent<PowerUpComponent>().bazoubulleExplosion, transform.position, transform.rotation), 0.35f);

			GetComponent<ParticleSystem>().Stop();
			Destroy(GetComponent<MeshRenderer>());
			Destroy(GetComponent<Collider>());

			Shake.ShakeEffect(Camera.main.gameObject, 0.25f, 0.12f);
		}
	}

	ArrayList GetPlayers()
	{
		return GameObject.FindGameObjectWithTag("WorldController").GetComponent<CheckAlives>().GetPlayers();
	}
}
