using UnityEngine;
using System.Collections;

public class hptest : MonoBehaviour {
	Texture greenz;
	Texture black;
	Texture orange;
	Vector3 screenPos;

	Player player;
	public float length = 80;
	float ojLength;
	float greenLength;

	float width = 8;
	public float yOffset = 50;
	// Use this for initialization
	void Start () {
		greenz = (Texture) Resources.Load ("Art/HP bar/green");
		black = (Texture) Resources.Load ("Art/HP bar/black");
		orange = (Texture) Resources.Load ("Art/HP bar/orange");

		player = gameObject.GetComponent<Player> ();
		ojLength = length;
		greenLength = length * (float)player.hp / player.maxHP;
	}

	void Update() {
		if (player.hp < player.maxHP) {
			if (ojLength > greenLength) {
				ojLength -= 50f * Time.deltaTime;
			}
		}
	}

	void OnGUI () {
		if (player.hp < player.maxHP) {
			screenPos = Camera.main.WorldToScreenPoint (transform.position);
			float xPos = screenPos.x - length / 2;
			float yPos = Screen.height - screenPos.y - yOffset;
			//black
			GUI.DrawTexture (new Rect(xPos, yPos, length, width), black , ScaleMode.StretchToFill, true, 1.0f);
			//orange
			GUI.DrawTexture (new Rect(xPos, yPos, ojLength, width), orange , ScaleMode.StretchToFill, true, 1.0f);
			getGreenLength();
			//green
			GUI.DrawTexture (new Rect(xPos, yPos, greenLength, width), greenz , ScaleMode.StretchToFill, true, 1.0f);
		}

	}

	void getGreenLength() {
		greenLength = length * (float)player.hp / player.maxHP;
		if (greenLength < 0) {
			greenLength = 0;
		}
	}
}
