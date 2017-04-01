using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
	public float speed = 6.0F;
	private Vector2 moveDirection = new Vector2();

	void Update()
	{
		Rigidbody2D body = GetComponent<Rigidbody2D>();

		moveDirection.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		body.AddForce(moveDirection);
	}
}
