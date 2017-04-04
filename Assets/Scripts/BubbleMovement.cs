using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
	public float acc = 25f;
	public float topSpeed = 5f;
	public int controllerId = 1;

	public GameObject collisionParticles;

	public float transitionRate = 0.90f;

	private Vector3 moveDirection;
	private float squish = 1;
	
	private Rigidbody body;

	private bool collisionHandled = false;
	private bool wasGiant = false;

	void Start()
	{
        body = GetComponent<Rigidbody>();
	}

	void Update()
	{
        GameObject gameObject = GameObject.Find("WorldController");
        if (gameObject.GetComponent<CheckAlives>().finished)
            return;
		
		bool isGiant = GetComponent<PowerUpComponent>().isGiant();

		if(!GetComponent<PlayerComponent>().alive)
			return;

		moveDirection.Set(Input.GetAxis("Horizontal_" + controllerId), 0, Input.GetAxis("Vertical_" + controllerId));

		if(moveDirection.magnitude > 0.1 && onGround())
		{
			transform.LookAt(moveDirection + transform.position);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			moveDirection *= acc;

			if(body.velocity.magnitude < topSpeed + (isGiant ? -1 : 0))
				body.AddForce(moveDirection);
		}

		float value = (float)Math.Pow(1f - transitionRate, Time.deltaTime);

		squish *= value;
		squish += 1f - value;

		transform.localScale = new Vector3(
                       isGiant ? 2f : 1f, 
					   (isGiant ? 2f : 1f) * (1f - Mathf.Sin(Time.time * 2 * Mathf.PI) / 20f - 0.05f), 
					   isGiant ? 2f * squish : squish);

		if(wasGiant && !isGiant)
			transform.position -= new Vector3(0, 0.5f, 0);

		wasGiant = isGiant;
		collisionHandled = false;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);

		if(squish > 0.9)
			squish = 0.5f;

		bool isGiant = GetComponent<PowerUpComponent>().isGiant();

		if(collision.gameObject.tag == "Player")
		{	
			if(collisionHandled)
				return;
	
			if(collisionParticles != null)
				Destroy(Instantiate(collisionParticles, transform.position, collisionParticles.transform.rotation), 5.1f);

			bool otherGiant = collision.gameObject.GetComponent<PowerUpComponent>().isGiant();
			
			if(isGiant && !otherGiant)
			{
				collision.rigidbody.velocity *= 2f;
				collision.gameObject.GetComponent<BubbleMovement>().collisionHandled = true;
				collisionHandled = true;
				return;
			}
			
			if(body.velocity.magnitude > collision.rigidbody.velocity.magnitude)
			{
				if(body.velocity.magnitude < 2)
				{
					collision.rigidbody.velocity = collision.rigidbody.velocity.normalized * 10;
					body.velocity = body.velocity.normalized * 15;
					return;
				}
				
				collision.rigidbody.velocity = -body.velocity;
				body.velocity *= 1.5f;
				collision.gameObject.GetComponent<BubbleMovement>().collisionHandled = true;
				collisionHandled = true;
				return;
			}
		}
	}

	bool onGround()
	{
		return true; //TODO not working
		//return Physics.Raycast(transform.position, -Vector3.up, GetComponent<SphereCollider>().radius + 1f);
	}
}
