using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3);
		rigidbody2D.velocity = new Vector2(10 *Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad), 0);
		//print (Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad));
		//print ("angle: " + transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
