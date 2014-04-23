using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int hp = 100;
	
	bool isDead = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollsionEnter2D ( Collision2D other) {
//		if (other.gameObject.CompareTag("playerProjectile")) {
//			takeDamage(20);
//		}
	
	}
	
	public void takeDamage(int damage) {
		
		hp-= damage;
		
		if (hp <= 0 ) {
			isDead = true;
			death();
		}
	}
	
	void death() {
	
		Destroy (gameObject);
	
	}
}
