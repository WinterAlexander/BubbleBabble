using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
	public float acc = 25f;
	public float topSpeed = 5f;
	public int controllerId = 1;


	public float transitionRate = 0.90f;

	private Vector3 moveDirection;
	private float squish = 1;
	
	private Rigidbody body;

	private Vector3? bubbleCollision;

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

		if(moveDirection.magnitude > 0.1)
		{
			//moveDirection = transform.TransformDirection(moveDirection);
			transform.LookAt(moveDirection + transform.position);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			moveDirection *= acc;


			if(body.velocity.magnitude < topSpeed + (isGiant ? -1 : 0))
				body.AddForce(moveDirection);
		}
		

		if(bubbleCollision != null)
		{
			body.AddForce(bubbleCollision.Value, ForceMode.Impulse);
			bubbleCollision = null;
		}
		

		float value = (float)Math.Pow(1f - transitionRate, Time.deltaTime);

		squish *= value;
		squish += 1f - value;

		transform.localScale = new Vector3(
                       isGiant ? 2f : 1f, 
					   (isGiant ? 2f : 1f) * (1f - Mathf.Sin(Time.time * 2 * Mathf.PI) / 20f - 0.05f), 
					   isGiant ? 2f * squish : squish);


        //body.mass = isGiant ? 2 * mass : mass;
		//collider.radius = isGiant ? 2 * radius : radius;
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.relativeVelocity.y > 0.5)
			return;

		if(squish > 0.9)
			squish = 0.5f;

		bool isGiant = GetComponent<PowerUpComponent>().isGiant();

		if(collision.gameObject.tag == "Player")
		{
			if(isGiant && !collision.gameObject.GetComponent<PowerUpComponent>().isGiant())
			{
				body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
				collision.rigidbody.velocity *= 2f;
				return;
			}

			if(body.velocity.magnitude < collision.rigidbody.velocity.magnitude)
			{
				if(collision.relativeVelocity.magnitude < 1)
					bubbleCollision = collision.relativeVelocity.normalized*10f;
				else
					bubbleCollision = collision.relativeVelocity*1.25f;
			}
		}
	}
}
