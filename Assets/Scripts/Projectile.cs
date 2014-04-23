using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public int speed = 15;
	public int damage = 10;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3);
		rigidbody2D.velocity = new Vector2(speed *Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad), 0);
		//print (Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad));
		//print ("angle: " + transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnCollisionEnter2D (Collision2D other) {
	
		if (other.gameObject.CompareTag("enemy")) {
			other.gameObject.GetComponent<Enemy>().takeDamage(damage);
		}
		Destroy(gameObject);
		
	}
}
