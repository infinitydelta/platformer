using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3 (0f, 0f, Random.Range (0f, 360f));
	}
}
