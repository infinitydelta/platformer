using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float yOffset = 2.5f;
	Vector3 pos;
	GameObject[] playerz;
	// Use this for initialization
	void Start () {
		playerz = GameObject.FindGameObjectsWithTag ("Player");
		//pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//center on multiple players
		pos = Vector3.zero;
		for (int i = 0; i < playerz.Length; i ++) {

			pos += playerz[i].transform.position;
		}
		pos /= playerz.Length;
		transform.position = (new Vector3(pos.x, pos.y + yOffset, -10) );
	}
}
