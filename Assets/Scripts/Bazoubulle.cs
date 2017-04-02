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
		GetComponent<Rigidbody>().velocity = baseVel;
        Shake.ShakeEffect(Camera.main.gameObject, 0.175f, 0.08f);
	}

	// Update is called once per frame
	void Update()
	{
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * baseVel.magnitude;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			if(collision.gameObject != holder)
				holder.GetComponent<Rigidbody>().AddExplosionForce(1000f, collision.contacts[0].point, 3f, 0);

			GetComponent<ParticleSystem>().Stop();
			Destroy(GetComponent<MeshRenderer>());
			Destroy(GetComponent<Collider>());
		}
	}
}
