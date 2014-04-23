using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	Vector3 pos;
	GameObject[] playerz;
	// Use this for initialization
	void Start () {
		playerz = GameObject.FindGameObjectsWithTag ("Player");
		//pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		pos = Vector3.zero;
		for (int i = 0; i < playerz.Length; i ++) {

			pos += playerz[i].transform.position;
		}
		pos /= playerz.Length;
		transform.position = (new Vector3(pos.x, pos.y, -10) );
	}
}
