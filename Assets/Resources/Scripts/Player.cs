using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int maxHP = 100;
	public int hp;


	bool moving, movingX, movingY, jumping, attacking, climbing;
	bool canClimb = true;

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
	public string down = "s";
	public string shoot = "j";

	float velX;
	int startingAccel = 75;
	int accel = 35;
	int climbSpeed = 5;

	Animator anim;
	// Use this for initialization
	void Start () {
		hp = maxHP;
		speed = startSpeed;
		climbing = false;

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
			if (!canClimb) {
				canClimb = true;
			}
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
		print (rigidbody2D.velocity.x);
	}

	void controls() {
		if(!attacking && !climbing)
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

		else if (climbing) {
			//climbing rope
			int dir = 0;
			if (Input.GetKey (left)) {
				dir = -1;
			}
			else if (Input.GetKey (right)) {
				dir = 1;				
			}
			else {
				dir = 0;
			}


			if (Input.GetKey(jump2)) {
				//move up
				//print ("moving up");
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, climbSpeed);

			}
			else if (Input.GetKey(down)) {
				//move down
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -climbSpeed);

			}

			if (Input.GetKeyDown(jump)) {
				//jump off	
				print ("JAMP");
				climbing = false;
				canClimb = false;
				rigidbody2D.gravityScale = 4;
				rigidbody2D.velocity = new Vector2(dir * 30, jumpStrength);
				movingY = true;
				jumping = true;
			}

			else if (!Input.GetKey(jump2) && !Input.GetKey(down)){ //not moving
				rigidbody2D.velocity = Vector2.zero;
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

		if (rigidbody2D.velocity.y <= -10) {
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -10);
		}
	}
	
	public void fire()
	{
			Camera.main.GetComponent<CameraScript>().shake = .05f;
	
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
	
	void takeDamage() {
		hp -= 10;
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag("enemyProjectile")) {
			int dir = (int)Mathf.Sign(transform.position.x - other.gameObject.transform.position.x);
			rigidbody2D.velocity = new Vector2( dir * 5,5);
			takeDamage();
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (Input.GetKey (jump2) && canClimb &&other.gameObject.CompareTag("rope")) {
			climbing = true;
			transform.position = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
			rigidbody2D.velocity = new Vector2(0,0);
			//print ("trying to climb");
			//rigidbody2D.isKinematic = true;
			rigidbody2D.gravityScale = 0;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		//print ("in collision");
		if (Input.GetKey (jump2) && canClimb &&other.gameObject.CompareTag("rope")) {
			climbing = true;
			transform.position = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
			rigidbody2D.velocity = new Vector2(0,0);
			//print ("trying to climb");
			//rigidbody2D.isKinematic = true;
			rigidbody2D.gravityScale = 0;
		}

	}

}
