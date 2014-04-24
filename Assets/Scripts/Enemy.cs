using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int hp = 100;
	public GameObject projectile;
	public GameObject blood;
	public GameObject bloodDeath;
	bool moving, movingX, movingY, jumping;
	float stoppingFriction = .85f;
	bool isDead = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moving = (movingX || movingY);
		
		movingX = false;


		//controls()
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

	void ai() {

	}
	
	void death() {
		Instantiate(bloodDeath, transform.position, transform.rotation);
		Destroy (gameObject);
	
	}
}
