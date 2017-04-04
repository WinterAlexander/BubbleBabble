using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public int playerId;
	public bool alive = true;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	public void Kill()
	{
		alive = false;
        gameObject.GetComponent<PowerUpComponent>().type = PowerUpType.NONE;
		//TODO animation ? sounds ?
	}
}
