using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;

	private float verticalVelocity;
	private float gravity = 14.0f;
	private float jumpForce = 8.0f;
	private float horizontalUpdatePos = 0;
	private float verticalUpdatePos = 0;

	private void Start () {
		controller = GetComponent<CharacterController> (); 
	}

	private void Update () {
		Vector3 moveVector = Vector3.zero;
		float xValue = 0;
		float zValue = 0;
	
		if (controller.isGrounded) {
			verticalVelocity = -gravity * Time.deltaTime;
			if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0 || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) {
				verticalVelocity = jumpForce;
				horizontalUpdatePos = 0;
				verticalUpdatePos = 0;
				float horizontalMove = Input.GetAxis ("Horizontal");
				float verticalMove = Input.GetAxis ("Vertical");

				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					// Get movement of the finger since last frame
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
					Debug.Log (touchDeltaPosition);
					horizontalMove = touchDeltaPosition.x;
					verticalMove = touchDeltaPosition.y;
				}
		
				if (horizontalMove > 0 && verticalMove < horizontalMove) {
					horizontalUpdatePos = 1;
				} else if (horizontalMove < 0 && verticalMove > horizontalMove) {
					horizontalUpdatePos = -1;
				} else if (verticalMove > 0) {
					verticalUpdatePos = 1;
				} else if (verticalMove < 0) {
					verticalUpdatePos = -1;
				}
			}
		} else {
			xValue = horizontalUpdatePos;
			zValue = verticalUpdatePos;
			verticalVelocity -= gravity * Time.deltaTime;
		}

		moveVector.x = xValue;
		moveVector.y = verticalVelocity;
		moveVector.z = zValue;

		controller.Move (moveVector * Time.deltaTime);
	}
}
