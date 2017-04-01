using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
	public float speed = 6.0F;
	public int controllerId = 1;

	private Vector3 moveDirection = new Vector3();
	private float distToGround;

	void Start()
	{
		distToGround = GetComponent<Collider>().bounds.extents.y;
	}

	void Update()
	{

		Rigidbody body = GetComponent<Rigidbody>();

		moveDirection.Set(Input.GetAxis("Horizontal_" + controllerId), 0, Input.GetAxis("Vertical_" + controllerId));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		body.AddForce(moveDirection);
	}

	bool isOnGround()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
}
