using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;
	public int timedelay = 4;
	float timer = 0;
	int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= timedelay && count < 10) {
			Instantiate(enemy, transform.position, transform.rotation);
			count++;
			timer = 0;
		}
	}
}
