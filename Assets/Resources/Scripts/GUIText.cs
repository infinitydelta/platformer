using UnityEngine;
using System.Collections;

public class GUIText : MonoBehaviour {
	
	//public Color color = Color(0.8, 0.8, 0, 1.0);
	public float scroll = 0.05f;
	public float duration = 1.5f;
	public float alpha;
	
	// Use this for initialization
	void Start () {
		alpha = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (alpha > 0) {
			transform.Translate(0, scroll*Time.deltaTime, 0);
			//transform.position = new Vector3(transform.position.x, transform.position.y += scroll * Time.deltaTime, transform.position.z) ;
			//transform.position.y += scroll * Time.deltaTime;
			alpha -= Time.deltaTime/duration;
			guiText.material.color = new Color(guiText.material.color.r, guiText.material.color.g, guiText.material.color.b, alpha);
		}
		else {
			Destroy(gameObject);
		}
	}
}
