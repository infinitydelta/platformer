using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int hp = 100;
	public int speed = 3;
	public GameObject projectile;
	public GameObject blood;
	public GameObject bloodDeath;

	int aggroDist = 30;

	float distanceTravelled = 0;
	bool moving, movingX, movingY, jumping;
	float stoppingFriction = .85f;
	bool isDead = false;
	bool mover, movel;

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

		moving = (movingX || movingY);
		
		movingX = false;


		//controls()
		attack (nearestPlayer);
		idle ();
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
	}
	
	void OnCollsionEnter2D ( Collision2D other) {
	
	}
	
	public void takeDamage(int damage) {
		Instantiate (blood,transform.position, transform.rotation);
		hp-= damage;
		
		if (hp <= 0 ) {
			isDead = true;
			death();
		}
	}
	void moveLeft() {
		rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
		distanceTravelled += speed * Time.deltaTime;
		movel = true;
		mover = false;
	}
	void moveRight() {
		rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);	
		distanceTravelled += speed * Time.deltaTime;
		mover = true;
		movel = false;
	}

	void attack(GameObject target) {
		if (Vector2.Distance (target.transform.position, transform.position) < aggroDist) {
			float dir = Mathf.Sign(target.transform.position.x- transform.position.x);
			rigidbody2D.velocity = new Vector2( dir*speed, rigidbody2D.velocity.y);	

		}
	}

	void ai() {
		if (distanceTravelled >= Random.Range (7, 20)) {
			if (mover) {
				moveLeft();
			}
			else {
				moveRight();
			}
		}
	}

	void idle() {

		if (distanceTravelled >= Random.Range (7, 20)) {
			if (mover) {
				moveLeft();
			}
			else {
				moveRight();
			}
		}
	}
	void death() {
		Instantiate(bloodDeath, transform.position, transform.rotation);
		Destroy (gameObject);
	
	}
}
