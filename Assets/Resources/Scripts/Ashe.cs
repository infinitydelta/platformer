using UnityEngine;
using System.Collections;

public class Ashe : MonoBehaviour {

	public GameObject volleyProj;
	
	public string ability1 = "u";
	
	
	float cd1 = .1f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		abilityChecker();
	}
	
	void abilityChecker() {
		volley();
	}
	
	public void volley() {
		if (Input.GetKeyDown (ability1)) {
			
			//rotation of projectile
			Quaternion rot = Quaternion.Euler(0,0,0);
			if (transform.localScale.x < 0 ) {
				rot = Quaternion.Euler(0,0,180);
			}
			for (int i = 0; i < 4; i++) {
				Instantiate (volleyProj, transform.position - new Vector3(0, .1f, 0) + new Vector3(transform.localScale.x * i * .2f, i*.1f,0) , rot); 
				
			}
			print ("working");
		}
	}
}
