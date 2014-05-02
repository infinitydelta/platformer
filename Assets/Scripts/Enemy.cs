using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int hp = 100;
	public int speed = 3;
	public int accel = 20;
	public GameObject hit;
	public GameObject bloodDeath;
	public GameObject proj;

	
	
	int aggroDist = 5;
	int attackRange = 4;

	float waitTime = 0;
	int movedist = 0;
	float distanceTravelled = 0;
	bool moving, movingX, movingY, jumping;
	float stoppingFriction = .85f;
	bool isDead = false;
	bool mover, movel;
	int setdir = 0;
	
	bool ready = true;
	
	bool waiting = false;
	float timer = 0;
	
	GameObject nearestPlayer;
	private GameObject[] myPlayers;


	// Use this for initialization
	void Start () {
		myPlayers = GameObject.FindGameObjectsWithTag ("Player");
		//farthestPlayer = 
		int i = Random.Range (0, 1);
		if (i == 1) {
			mover = true;
		} else {
			movel = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		findNearestPlayer();

		moving = (movingX || movingY);
		
		movingX = false;

		if (!ready) {
			timer+= Time.deltaTime;
			if (timer >= 3) {
				ready = true;
				timer = 0;
			}
		}


		//controls()
		attack (nearestPlayer);
		//idle ();
		if (rigidbody2D.velocity.y == 0) {
			movingY = false;
		} else {
			movingY = true;
		}



		if (!movingX) { //stop
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * stoppingFriction, rigidbody2D.velocity.y);
		}
		//limit fall velocity
		if (rigidbody2D.velocity.y <= -10) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -10);
		}
		
		limitSpeed();
	}
	
	void OnCollsionEnter2D ( Collision2D other) {
	
	}
	
	void findNearestPlayer() {
		//find players
		if (nearestPlayer == null) {
			nearestPlayer = myPlayers[0];	
		}
		
		for (int i = 0; i < myPlayers.Length; i ++) {
			if (Vector2.Distance(myPlayers[i].transform.position, transform.position) < aggroDist) {
				if (Vector2.Distance(myPlayers[i].transform.position, transform.position) <
				    (Vector2.Distance(nearestPlayer.transform.position, transform.position))) {
					
					nearestPlayer = myPlayers[i];
				}
			}
		}
	
	}
	
	public void takeDamage(int damage) {
		Instantiate (hit,transform.position, transform.rotation);
		hp-= damage;
		
		if (hp <= 0 ) {
			isDead = true;
			death();
		}
	}
	void moveLeft() {
		rigidbody2D.AddForce(new Vector2(-accel, 0));
		distanceTravelled += speed * Time.deltaTime;
		movel = true;
		mover = false;
	}
	void moveRight(int dist) {
		rigidbody2D.AddForce(new Vector2(accel, 0));
		distanceTravelled += speed * Time.deltaTime;
		print (distanceTravelled);
		mover = true;
		movel = false;
	}
	
	void move(float dist) {
		//actually travels a bit less than distanceTravelled due to acceleration
		int dir = (int) Mathf.Sign (dist);
		rigidbody2D.AddForce(new Vector2(dir * accel, 0));
		distanceTravelled += speed * Time.deltaTime;
		transform.localScale = new Vector3(dir, 1, 1);
		movingX = true;
	
	}

	void attack(GameObject target) {
		float dist = Vector2.Distance (target.transform.position, transform.position);
		int dir = (int) Mathf.Sign(target.transform.position.x- transform.position.x);
		
		
		if (dist < aggroDist) { //notice player
		
			//in range to attack
			//fire
			if (dist <= attackRange) {
				fire (dir);
			}
			
			if (dist <= attackRange * .5f) {
				//try firing
				//fire(dir);
				
				if (setdir == 0) {
					setdir = (int) dir;
				}
				move (setdir);
			}
			else {
				//too far, move towards player
				move (dir);
				setdir = 0;
			}
			/*
			if (dist <= attackRange && ready) { //in attack range and can attack
				fire ();
				
				//print ("in attack range");
			}
			//attack not ready, orbit player
			else if (dist <= attackRange && !ready){
				if (distanceTravelled < Mathf.Abs(dist*2)) {
					move( dir );
				}
				//if (dir != olddir) {
					
				//}
				
				//else 
				//int olddir
				move (dir*attackRange);
				print ("dist traveled: " + distanceTravelled);
				//print ("not ready" + dist);
				//moveRight(5);
				//rigidbody2D.AddForce(new Vector2(-dir*accel, 0));
				
			}
			*/
			// move past them
//			else {
//				rigidbody2D.AddForce(new Vector2(dir*accel, 0));
//				
//			}
			
		}
		
		else {
			idle ();
		}
	}

	void ai() {
		if (distanceTravelled >= Random.Range (7, 20)) {
			if (mover) {
				moveLeft();
				distanceTravelled = 0;
			}
			else {
				moveRight(5);
				distanceTravelled = 0;
			}
		}
	}

	void idle() {
		setdir = 0;
		if (movedist ==0) {
			movedist = Random.Range(-5, 5); //Random.Range(-3, -10);
			print ("movedist: " + movedist);
		}
		

		//travelled the whole distance, now wait
		if (distanceTravelled >= Mathf.Abs( movedist)) {
			float randWait = Random.Range(.5f, 3f);
			wait(randWait);
			//print ("travelled: " + distanceTravelled);
			//distanceTravelled = 0;
			if (!waiting) {
				movedist = Random.Range(-5, 5);
			}
			//reset moving
		}
		
		if (!waiting) {
			print ("moving");
			//movedist = Random.Range(-3, -10);
			move(movedist);
			
		}
	}
	
	void wait(float time) {
		waiting = true;
		waitTime += Time.deltaTime;
		print ("waiting");
		if (waitTime >= time) {
			print ("waittime: " + waitTime);
			waiting = false;
			distanceTravelled = 0;
			waitTime = 0;
		}
	
	}
	
	public void fire(int dir)
	{
		if (ready) {
			transform.localScale = new Vector3(dir, 1, 1);
		
			Quaternion rot = Quaternion.Euler(0,0,0);
			if (transform.localScale.x < 0 ) {
				rot = Quaternion.Euler(0,0,180);
			}
			
			Instantiate (proj, transform.position, rot); 
			ready = false;
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
	
	
	void death() {
		Instantiate(bloodDeath, transform.position, transform.rotation);
		Destroy (gameObject);
	
	}
}
