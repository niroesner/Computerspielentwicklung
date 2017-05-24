using UnityEngine;
using System.Collections;

public class FrogController : MonoBehaviour {
	
	public Vector3 nextDir;
	
	public float jumpForce=100;
	
	public float speed=5;
	public float speedRot=100;
	
	public float rotationOffset;
	
	Rigidbody rb;
	
	public Vector3 curPosition;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		
		curPosition=transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(transform.position!= new Vector3(curPosition.x,transform.position.y,curPosition.z) +nextDir)
		{
			
			transform.position=Vector3.MoveTowards (transform.position, new Vector3 (curPosition.x, transform.position.y, curPosition.z) + nextDir, speed * Time.deltaTime);
			
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (Quaternion.Euler(0,rotationOffset,0)*nextDir), speedRot * Time.deltaTime);
			
		}else{
			nextDir = Vector3.zero;
			curPosition=transform.position;
			curPosition.x = Mathf.Round (curPosition.x);
			curPosition.y = Mathf.Round (curPosition.y);
			
			
			if (Input.GetAxisRaw ("Horizontal") != 0) {
				
				nextDir.z = -Input.GetAxisRaw ("Horizontal");
				Move ();
				
			} else if (Input.GetAxisRaw ("Vertical") != 0) {
				
				nextDir.x = Input.GetAxisRaw ("Vertical");
				Move ();

			} else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				Vector2 touchDP = Input.GetTouch (0).deltaPosition;

				if(Mathf.Abs(touchDP.x) > Mathf.Abs(touchDP.y)) {
					//gesture had more horizonal movement
					if(touchDP.x < 0) {
						//left
						nextDir.z = 1;
					}
					else {
						//right
						nextDir.z = -1;
					}
				}
				else {
					//gesture had more vertical movement
					if(touchDP.y < 0) {
						//up
						nextDir.x = -1;
					}
					else {
						//down
						nextDir.x = 1;
					}
				}
				Move ();
			}
		}
	}
	
	
	void Move()
	{
		rb.AddForce (0, jumpForce, 0);
	}
}