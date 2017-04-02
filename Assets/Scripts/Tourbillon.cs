using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
	public float reach = 4f;
	public float intensity = 1f;

	public Vector3 velocity;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.forward, 270 * Time.deltaTime);

		transform.position += velocity * Time.deltaTime;

		foreach(GameObject player in GameObject.Find("WorldController").GetComponent<CheckAlives>().GetPlayers())
		{
			Vector3 drag = transform.position - player.transform.position;

			drag.y = 0;
			
			if(drag.magnitude > reach)
				continue;
			
			float force = drag.magnitude > 1 ? 1 / drag.magnitude : drag.magnitude;

			drag.Normalize();
			drag *= force * intensity;

			player.GetComponent<Rigidbody>().AddForce(drag, ForceMode.VelocityChange);
		}
	}
}
