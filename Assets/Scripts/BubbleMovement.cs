using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
	public float acc = 25f;
	public float topSpeed = 5f;

	public GameObject collisionParticles;

	public float transitionRate = 0.90f;

	private Vector3 moveDirection;
	private float squish = 1;
	
	private Rigidbody body;

	private bool collisionHandled = false;

    private int controllerId;

	void Start()
	{
        body = GetComponent<Rigidbody>();
        controllerId = GetComponent<PlayerComponent>().playerId;
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

		if(moveDirection.magnitude > 0.1)
		{
			transform.LookAt(moveDirection + transform.position);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			moveDirection *= acc;

            if(!onGround())
                moveDirection *= 0.1f;


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
        
		collisionHandled = false;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);

		if(squish > 0.9)
			squish = 0.5f;

		bool isGiant = GetComponent<PowerUpComponent>().isGiant();

        if(collision.gameObject.tag != "Player")
            return;
        
		if(collisionHandled)
			return;
	
		if(collisionParticles != null)
			Destroy(Instantiate(collisionParticles, transform.position, collisionParticles.transform.rotation), 5.1f);

		bool otherGiant = collision.gameObject.GetComponent<PowerUpComponent>().isGiant();

        float thisModifier = (isGiant ? 0.5f : 1) * (otherGiant ? 1.5f : 1);
        float otherModifier = (otherGiant ? 0.5f : 1) * (isGiant ? 1.5f : 1);

        if (body.velocity.magnitude > collision.rigidbody.velocity.magnitude)
		{
			if(body.velocity.magnitude < 5)
			{
				collision.rigidbody.velocity = -body.velocity.normalized * 3 * otherModifier;
				body.velocity = body.velocity.normalized * 5 * thisModifier;
				return;
			}
				
			collision.rigidbody.velocity = body.velocity * -otherModifier;
			body.velocity *= 1.5f * thisModifier;
			collision.gameObject.GetComponent<BubbleMovement>().collisionHandled = true;
			collisionHandled = true;
		}
	}

	bool onGround()
	{
		return Physics.Raycast(transform.position, -Vector3.up, GetComponent<SphereCollider>().radius + 1f);
	}
}
