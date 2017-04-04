using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraComponent : MonoBehaviour
{
	private CheckAlives checkAlives;
	private Vector3 pos, softPos;

	public float transitionRate = 0.95f;

	// Use this for initialization
	void Start()
	{
		checkAlives = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>();
	}
	
	// Update is called once per frame
	void Update()
	{
		pos.Set(0, 0, 0);

        float avgX = 0, avgY = 0, avgZ = 0;

        ArrayList players = checkAlives.GetPlayers();


        foreach (GameObject player in players)
        {
            avgX += player.transform.position.x;
            avgY += player.transform.position.y;
            avgZ += player.transform.position.z;
        }

        int count = players.Count;

        avgX /= count;
        avgY /= count;
        avgZ /= count;

        transform.position = new Vector3(avgX, avgY, avgZ);
        transform.position -= transform.forward * 8;

        while(!BoundsInCamera(players))
        {
             transform.position -= transform.forward;
        }

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
