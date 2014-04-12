using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	bool moving, movingX, movingY, jumping;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		controls ();
	}

	void controls() {
		if (Input.GetKey (KeyCode.A)) {
			//transform.Rotate(
			transform.rotation = Quaternion.Euler(0,0,180);
			rigidbody2D.velocity = new Vector2(-10, rigidbody2D.velocity.y);	
		}
		if (Input.GetKey (KeyCode.D)) {
			//face left
			transform.rotation = Quaternion.Euler(0,0,0);
			rigidbody2D.velocity = new Vector2(10, rigidbody2D.velocity.y);	
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			//rigidbody2D.AddForce(new Vector2(0, 200));
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 12);
			jumping = true;
		}
	}
}
