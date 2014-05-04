using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	public float minSize = 4f;
	public float extraSize = 1f;
	public float yOffset = 2f;
	
	public float easeAmount = 10f;
	public GameObject[] myPlayers;
	
	private Vector3 targetPosition;
	private float targetSize;
	
	
	
	
	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	Vector3 originalPos;
	
	// Use this for initialization
	void Start () {
		originalPos = transform.position;

		//myPlayers = GameObject.FindGameObjectsWithTag ("Player");
		//pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		myPlayers = GameObject.FindGameObjectsWithTag ("Player");
		
		
		if (shake > 0)
		{
			//
			transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			originalPos = transform.position;
			//transform.position = originalPos;
		}
		
		
		
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