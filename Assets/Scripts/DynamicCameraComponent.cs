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
		//transform.eulerAngles.Set(0, transform.eulerAngles.y, 0);
	}
}
