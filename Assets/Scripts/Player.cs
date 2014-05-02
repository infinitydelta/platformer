using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	bool moving, movingX, movingY, jumping, attacking;

	public int startSpeed = 7;
	public int speed;
	public float stoppingFriction = .85f;
	public GameObject projectile;
	public GameObject gunTip;
	public int jumpStrength = 16;
	//
	public string left = "a";
	public string right ="d";
	public string jump = "space";
	public string jump2 = "w";
	public string shoot = "j";

	float velX;
	int startingAccel = 35;
	int accel = 35;

	Animator anim;
	// Use this for initialization
	void Start () {
		speed = startSpeed;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		moving = (movingX || movingY);
		anim.SetBool("moving", moving);

		movingX = false;

		
		controls ();
		limitSpeed();
		
		if (rigidbody2D.velocity.y == 0) {
			movingY = false;
		} else {
			movingY = true;
		}
		
		//if jumping
		if (movingY) {
			//velX = rigidbody2D.velocity.x; //transform.localScale.x * speed;//
			
		}
		
		
		
		if (movingY) {
			accel = startingAccel /2;
		}
		else {
			accel = startingAccel;
		} 

		if (!movingX && !movingY) { //stop
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * stoppingFriction, rigidbody2D.velocity.y);
		}
		//rigidbody2D.AddForce (new Vector2 (0, -500));
		if (rigidbody2D.velocity.y <= -10) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -10);
		}
	}

	void controls() {
		if(!attacking)
		{
			if (Input.GetKey (left)) {
				movingX = true;
				transform.localScale = new Vector3(-1,1,1);
				//rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);	
				rigidbody2D.AddForce(new Vector2(-accel, 0));
			}
			if (Input.GetKey (right)) {
				movingX = true;
				transform.localScale = new Vector3(1,1,1);
				//rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);	
				rigidbody2D.AddForce(new Vector2(accel, 0));
				
			}
	
			if ((Input.GetKey(jump) || Input.GetKey(jump2))&& !movingY) {
				velX = rigidbody2D.velocity.x;
				rigidbody2D.velocity = new Vector2(velX, jumpStrength);
				movingY = true;
				jumping = true;
			}
			if (Input.GetKeyDown (shoot)) {
				anim.SetTrigger("Attack");
				attacking = true;
				
			}
		
		}
		


	}
	
	void limitSpeed() {
		if (rigidbody2D.velocity.x > speed) {
			rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
		}
		
		if (rigidbody2D.velocity.x < -speed) {
			rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
		}
	}
	
	public void fire()
	{
			Camera.main.GetComponent<CameraScript>().shake = .1f;
	
			Quaternion rot = Quaternion.Euler(0,0,0);
			if (transform.localScale.x < 0 ) {
				rot = Quaternion.Euler(0,0,180);
				//print (rot.eulerAngles.z);
			}
			
			Instantiate (projectile, gunTip.transform.position, rot); 
		
	}
	
	
	public void attackingFalse()
	{
		attacking = false;
	}
	
	
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag("enemy")) {
			int dir = (int)Mathf.Sign(transform.position.x - other.gameObject.transform.position.x);
			rigidbody2D.velocity = new Vector2( dir * 5,5);
		}
	}
}
