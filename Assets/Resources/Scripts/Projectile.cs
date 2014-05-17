using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public int speed = 15;
	public int damage = 10;
	public GameObject damageGUI;
	public GameObject hit;
	int i = 0;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3);
		rigidbody2D.velocity = new Vector2(speed *Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad), 0);
		//print (Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad));
		//print ("angle: " + transform.eulerAngles.z);
		for (int i = 0; i < 20; i ++) {
			GameObject hitparticle = (GameObject) Instantiate (Resources.Load("Prefabs/projectile smoke"), transform.position + Random.insideUnitSphere * .25f, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		GameObject hitparticle = (GameObject) Instantiate (Resources.Load("Prefabs/projectile smoke"), transform.position + Random.insideUnitSphere * .1f, transform.rotation);
	}
	
	void FixedUpdate() {
		//GameObject hitparticle = (GameObject) Instantiate (Resources.Load("Prefabs/projectile smoke"), transform.position, transform.rotation);
		
	}
	
	void OnCollisionEnter2D (Collision2D other) {
	
		if (other.gameObject.CompareTag("enemy")) {
			other.gameObject.GetComponent<Enemy>().takeDamage(this.gameObject);
		}
		Destroy(gameObject);
		
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("enemy")) {
			other.gameObject.GetComponent<Enemy>().takeDamage(this.gameObject);
			Vector3 guiPos = Camera.main.WorldToViewportPoint(other.transform.position);
			spawnGUI("100", guiPos.x, guiPos.y);
		}
		print ("hit " + i);
		i++;
		Destroy(gameObject);
	}
	
	void spawnGUI(string text, float x, float y) {
		//x = Mathf.Clamp(x, .05f, .95f);
		//y = Mathf.Clamp(y, .05f, .95f);
		GameObject gui = (GameObject) Instantiate(damageGUI, new Vector3(x,y,0), Quaternion.identity);
		gui.guiText.text = text;
		
	}
}
