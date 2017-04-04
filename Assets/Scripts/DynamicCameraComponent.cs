using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraComponent : MonoBehaviour
{
	private CheckAlives checkAlives;
	private Vector3 softPos;

	public float transitionRate = 0.95f;

	// Use this for initialization
	void Start()
	{
		checkAlives = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>();
        softPos = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
        Vector3 avg = new Vector3();

        ArrayList players = checkAlives.GetPlayers();

        foreach (GameObject player in players)
            avg += player.transform.position;

        avg /= players.Count;

        transform.position = avg - transform.forward * 8;

        while(!BoundsInCamera(players))
             transform.position -= transform.forward;

        float value = Mathf.Pow(1f - transitionRate, Time.deltaTime);
    
        softPos *= value;
        softPos += transform.position * (1f - value);

        transform.position = softPos;
    }

    private bool BoundsInCamera(ArrayList players)
    {
        foreach(GameObject player in players)
        {
            Vector3 vp = GetComponent<Camera>().WorldToViewportPoint(player.transform.position);

            if(vp.x < 0.25f || vp.x > 0.75f || vp.y < 0.25f || vp.y > 0.75f || vp.z < 0)
                return false;
        }
        return true;
    }
}
