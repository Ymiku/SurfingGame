﻿using UnityEngine;
using System.Collections;

public class BoardController_Ver5 : MonoBehaviour {
	// ここから
	public Vector3 moveSpeed = new Vector3(4,4,8);
	public bool isGround = false;
	public Transform boardObject;
	public Vector3 targetAngle;

	// Use this for initialization
	void Start () {
	
	}
	
	private float xForce;
	private bool isJump;
	
	void Update () {
		xForce = Input.GetAxisRaw("Horizontal");
		
		Vector3 nowAngle = boardObject.localRotation.eulerAngles;
		nowAngle.y = Mathf.LerpAngle( nowAngle.y, targetAngle.y * xForce, 0.1f );
		nowAngle.z = Mathf.LerpAngle( nowAngle.z, targetAngle.z * xForce, 0.1f );
		boardObject.localRotation = Quaternion.Euler(nowAngle);

		isJump = false;
		if ( Input.GetButton("Jump") && isGround ) isJump = true;
	}
	
	void FixedUpdate()
	{
		Vector3 vel = this.rigidbody.velocity;
		vel.x = moveSpeed.x * xForce;
		vel.z = moveSpeed.z;
		
		if ( isJump ) {
			vel.y += moveSpeed.y;
			isGround = false;
			isJump = false;
		}
		
		this.rigidbody.velocity = vel;
	}

	void OnCollisionEnter( Collision col ) 
	{
		// check ground for a water effect
		if ( col.gameObject.CompareTag("Ground") ) isGround = true;
	}
	// ここまで
}
