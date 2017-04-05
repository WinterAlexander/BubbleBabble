using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullePoundComponent : MonoBehaviour
{
	private bool pounding = false;
	
	public GameObject poundParticles;
	
	private GameObject worldController;
	
	void Start()
	{
		worldController = GameObject.Find("WorldController");
	}
	
	void Update()
	{
		if(!pounding || !GetComponent<BubbleMovement>().onGround())
			return;
		
		Destroy(Instantiate(poundParticles, transform.position, poundParticles.transform.rotation), 2.1f);
		
		foreach(GameObject player in worldController.GetComponent<CheckAlives>().GetPlayers())
		{
			if(player == gameObject)
				continue;
			
			Vector3 shootDir = player.transform.position - transform.position;
			
			player.GetComponent<Rigidbody>().AddForce(shootDir.normalized * 10f, ForceMode.Impulse);
		}
		
		pounding = false;
	}
	
	public void Pound()
	{
		pounding = true;
	}
	
}
