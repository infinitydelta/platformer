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
	float distToTravel = 0;
	bool moving, movingX, movingY, jumping;
	float stoppingFriction = .85f;
	bool isDead = false;
	bool mover, movel;
	int setdir = 0;
	
	bool ready = true;
	
	bool waiting = false;
	float timer = 0;
	float waitTimer = 0;
	
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
		
		//moving
		/*
		if (movingX) {
			//gotta move
			if (distanceTravelled < distToTravel) {
				print ("distance travelled: " + distanceTravelled);
				int dir = (int) Mathf.Sign (distToTravel);
				rigidbody2D.AddForce(new Vector2(dir * accel, 0));
				distanceTravelled += speed * Time.deltaTime;
			}
			else { //moved already
				distanceTravelled = 0;
				distToTravel = 0;
				movingX = false;
			}
		}*/
		
		moving = (movingX || movingY);		
		movingX = false;

		//fire cooldown
		if (!ready) {
			timer+= Time.deltaTime;
			if (timer >= 3) {
				ready = true;
				timer = 0;
			}
		}


		attack (nearestPlayer);
		
		
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
		
		//wait
		if (waiting) {
			waitTime += Time.deltaTime;
			if (waitTime >= waitTimer) {
				print ("waittime: " + waitTime);
				waiting = false;
				distanceTravelled = 0;
				waitTime = 0;
				waitTimer = 0;
			}
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
	
	void moveDistance(float distance) {
		//set target location
		//add force to get to t location
		//once reached, set movingToTargetLocation = false
	}


	void move(float dist) {
		//actually travels a bit less than distanceTravelled due to acceleration		
		
		if (!waiting) {
			int dir = (int) Mathf.Sign (dist);
			rigidbody2D.AddForce(new Vector2(dir * accel, 0));
			distanceTravelled += speed * Time.deltaTime;
			transform.localScale = new Vector3(dir, 1, 1);
			movingX = true;
		}
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
				//orbit				
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
			
		}
		
		else if (dist < 30) { //idle
			idle ();
		}
		
		//too far don't move
		else {
			
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
			move(movedist);
			
		}
	}
	
	void wait(float time) {
		waiting = true;
		waitTimer = time;
		//print ("waiting");
		
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
	
	public void takeDamage(GameObject proj) {
		GameObject hitparticle = (GameObject) Instantiate (hit, proj.transform.position, transform.rotation);
		//hitparticle.transform.parent = transform;
		//hp-= damage;
		float dist = Vector2.Distance (nearestPlayer.transform.position, transform.position);
		int dir = (int) Mathf.Sign(proj.rigidbody2D.velocity.x);
		
		if (dist > aggroDist) {
			move (dir);
		}
		
		//rigidbody2D.velocity = new Vector2( dir * 15,5);
		rigidbody2D.AddForce(new Vector2(dir*1000, 0));
		wait (.25f);
		if (hp <= 0 ) {
			isDead = true;
			death();
		}
	}
	
	void death() {
		Instantiate(bloodDeath, transform.position, transform.rotation);
		Destroy (gameObject);
	
	}
}
