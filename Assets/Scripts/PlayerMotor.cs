using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;

	private float verticalVelocity;
	private float horizontalVelocity;
	private float gravity = 14.0f;
	private float jumpForce = 8.0f;


	private void Start () {
		controller = GetComponent<CharacterController> (); 
	}

	private void Update () {
		if (controller.isGrounded) {
			verticalVelocity = -gravity * Time.deltaTime;

			if (Input.GetKeyDown (KeyCode.Space) || Input.touchCount > 0) {
				verticalVelocity = jumpForce;
				horizontalVelocity = jumpForce * 2.0f;
			}
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
			horizontalVelocity = 0;
		}

		Vector3 moveVector = new Vector3 (horizontalVelocity, verticalVelocity, 0);
		controller.Move (moveVector * Time.deltaTime);
	}
}
