using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	public float minSize = 4f;
	public float extraSize = 1f;
	public float yOffset = 2f;
	
	public float easeAmount = 10f;
	private GameObject[] myPlayers;
	
	private Vector3 targetPosition;
	private float targetSize;
	// Use this for initialization
	void Start () {
		myPlayers = GameObject.FindGameObjectsWithTag ("Player");
		//pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//center on multiple players
		float leftmostPos = myPlayers[0].transform.position.x;
		float rightmostPos = myPlayers[0].transform.position.x;
		float topmostPos = myPlayers[0].transform.position.y;
		float bottommostPos = myPlayers[0].transform.position.y;
		float xsum = 0;
		float ysum = 0;
		for (int i = 0; i < myPlayers.Length; i ++) {
			if (myPlayers[i].transform.position.x < leftmostPos) {
				leftmostPos = myPlayers[i].transform.position.x;
			}
			if (myPlayers[i].transform.position.x > rightmostPos) {
				rightmostPos = myPlayers[i].transform.position.x;
			}
			if (myPlayers[i].transform.position.y < bottommostPos) {
				bottommostPos = myPlayers[i].transform.position.y;
			}
			if (myPlayers[i].transform.position.y > topmostPos) {
				topmostPos = myPlayers[i].transform.position.y;
			}
			xsum += myPlayers[i].transform.position.x;
			ysum += myPlayers[i].transform.position.y;
		}
		targetSize = ((rightmostPos - leftmostPos) / 2f) + extraSize;
		if (targetSize < minSize)
			targetSize = minSize;
		targetPosition = new Vector3(xsum / myPlayers.Length, (ysum / myPlayers.Length) + yOffset, -10f);
		camera.orthographicSize += (targetSize - camera.orthographicSize) / easeAmount;
		transform.position += (targetPosition - transform.position) / easeAmount;
	}
}