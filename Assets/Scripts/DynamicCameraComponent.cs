using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraComponent : MonoBehaviour
{
	private CheckAlives checkAlives;
	private Vector3 pos, softPos, original;

	public float transitionRate = 0.95f;

	// Use this for initialization
	void Start()
	{
		checkAlives = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>();
		original = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		pos.Set(0, 0, 0);

        float minX, maxX;
        float minY, maxY;
        float minZ, maxZ;

		if(checkAlives.GetPlayers().Count != 0)
		{
			foreach(GameObject player in checkAlives.GetPlayers())
				pos += player.transform.position;

			pos /= checkAlives.GetPlayers().Count*5;
		}

		float value = (float)Math.Pow(1f - transitionRate, Time.deltaTime);

		softPos *= value;
		softPos += pos * (1f - value);

		transform.position = original + softPos;
        transform.position = new Vector3(transform.position.x,
                                        transform.position.y + Math.Abs(transform.position.x) + Math.Abs(transform.position.z),
                                        transform.position.z - 8);
        //TODO better cam, this is temp since last cam we couldnt see shit in map
		//transform.eulerAngles.Set(0, transform.eulerAngles.y, 0);
	}
}
