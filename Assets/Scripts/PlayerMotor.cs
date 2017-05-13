using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;

	private float verticalVelocity;
	private float gravity = 14.0f;
	private float jumpForce = 8.0f;
	private float horizontalMove = 0;
	private float verticalMove = 0;

	private void Start () {
		controller = GetComponent<CharacterController> (); 
	}

	private void Update () {
		Vector3 moveVector = Vector3.zero;
		float xValue = 0;
		float zValue = 0;
	
		if (controller.isGrounded) {
			verticalVelocity = -gravity * Time.deltaTime;
			if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0 || Input.touchCount > 0) {
				verticalVelocity = jumpForce;
				horizontalMove = 0;
				verticalMove = 0;

				Debug.Log (Input.GetTouch (0).deltaPosition);

				if (Input.GetAxis ("Horizontal") > 0) {
					horizontalMove = 1;
				} else if (Input.GetAxis ("Horizontal") < 0) {
					horizontalMove = -1;
				} else if (Input.GetAxis ("Vertical") > 0) {
					verticalMove = 1;
				} else if (Input.GetAxis ("Vertical") < 1) {
					verticalMove = -1;
				}
			}
		} else {
			xValue = horizontalMove;
			zValue = verticalMove;
			verticalVelocity -= gravity * Time.deltaTime;
		}

		moveVector.x = xValue;
		moveVector.y = verticalVelocity;
		moveVector.z = zValue;

		controller.Move (moveVector * Time.deltaTime);
	}
}
