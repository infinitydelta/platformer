using UnityEngine;
using System.Collections;

public class JumpPortal : MonoBehaviour {
	public int jumpVel = 25;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.rigidbody2D.velocity = new Vector2(other.gameObject.rigidbody2D.velocity.x, jumpVel);
		}
	}
}
