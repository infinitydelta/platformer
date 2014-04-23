using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	bool moving, movingX, movingY, jumping;

	public int regularSpeed = 5;
	public int speed;
	public float stoppingFriction = .85f;
	public GameObject projectile;
	public GameObject gunTip;
	// Use this for initialization
	void Start () {
		speed = regularSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		moving = (movingX || movingY);

		movingX = false;

		controls ();
		if (rigidbody2D.velocity.y == 0) {
			movingY = false;
		} else {
			movingY = true;
		}
		/*
		if (movingY) {
			speed = regularSpeed /2;
		}
		else {
			speed = regularSpeed;
		} */

		if (!movingX) { //stop
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * stoppingFriction, rigidbody2D.velocity.y);
		}
		//rigidbody2D.AddForce (new Vector2 (0, -500));
		if (rigidbody2D.velocity.y <= -10) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -10);
		}
	}

	void controls() {
		if (Input.GetKey (KeyCode.A)) {
			movingX = true;
			transform.localScale = new Vector3(-1,1,1);
			rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);	
		}
		if (Input.GetKey (KeyCode.D)) {
			movingX = true;
			transform.localScale = new Vector3(1,1,1);
			rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);	
		}

		if (Input.GetKey(KeyCode.Space) && !movingY) {
			//rigidbody2D.AddForce(new Vector2(0, 400));
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 16);
			movingY = true;
			jumping = true;
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			Quaternion rot = Quaternion.Euler(0,0,0);
			if (transform.localScale.x < 0 ) {
				rot = Quaternion.Euler(0,0,180);
				//print (rot.eulerAngles.z);
			}
		
			Instantiate (projectile, gunTip.transform.position, rot); 
		}
	}
}
