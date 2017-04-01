﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
	public float speed = 15f;
	public int controllerId = 1;


	public float transitionRate = 0.95f;

	private Vector3 moveDirection;
	private float colliderRadius, squish = 1;
	private SphereCollider collider;

	void Start()
	{
		collider = GetComponent<SphereCollider>();
		colliderRadius = collider.radius;
	}

	void Update()
	{
		if(!GetComponent<PlayerComponent>().alive)
			return;


		Rigidbody body = GetComponent<Rigidbody>();

		moveDirection.Set(Input.GetAxis("Horizontal_" + controllerId), 0, Input.GetAxis("Vertical_" + controllerId));
		//moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		body.AddForce(moveDirection);
		

		float value = (float)Math.Pow(1f - transitionRate, Time.deltaTime);

		squish *= value;
		squish += 1f - value;

		transform.localScale = new Vector3(1f, 1f, squish);

		//collider.radius = colliderRadius * squish;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.relativeVelocity.y > collision.relativeVelocity.magnitude * 0.5)
			return;

		Vector3 center = new Vector3();

		foreach(ContactPoint contact in collision.contacts)
		{
			center += contact.point;
		}

		center /= collision.contacts.Length;

		squish = 0.5f;
		transform.LookAt(center);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

		//transform.localScale.Set(10, 10, 10);
		//foreach(ContactPoint contact in collision.contacts)
		//{
		//Debug.DrawRay(contact.point, contact.normal, Color.white);
		//}
		//if(collision.relativeVelocity.magnitude > 2)
		//audio.Play();
	}
}
