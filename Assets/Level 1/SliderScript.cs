using UnityEngine;
using System.Collections;

public class SliderScript : MonoBehaviour {

	public GameObject player;
	public bool touchingL;
	public bool touchingR;

	void Start () {
		player = GameObject.Find ("Minute Man");
	}

	void Update () {
		
		if ((player.transform.position.x > this.transform.position.x - 5) && (player.transform.position.x < this.transform.position.x - 4) && (player.transform.position.y > this.transform.position.y - 1) && (player.transform.position.y < this.transform.position.y + .7f)) {
			touchingL = true;
		}
		if ((player.transform.position.x < this.transform.position.x - 5) || (player.transform.position.x > this.transform.position.x - 4) || (player.transform.position.y < this.transform.position.y - 1) || (player.transform.position.y > this.transform.position.y + .7f)) {
			touchingL = false;
		}

		if ((player.transform.position.x < this.transform.position.x + 5) && (player.transform.position.x > this.transform.position.x + 4) && (player.transform.position.y > this.transform.position.y - 1) && (player.transform.position.y < this.transform.position.y + .7f)) {
			touchingR = true;
		}
		if ((player.transform.position.x > this.transform.position.x + 5) || (player.transform.position.x < this.transform.position.x + 4) || (player.transform.position.y < this.transform.position.y - 1) || (player.transform.position.y > this.transform.position.y + .7f)) {
			touchingR = false;
		}

		if (touchingL && Input.GetKey(KeyCode.RightArrow) && (this.transform.position.x >= 10.4f)) {
			if (this.transform.position.x < 24.5) {
				this.transform.Translate(new Vector3 (.1f, 0f, 0f));
			}
		}
		
		if (touchingR && Input.GetKey (KeyCode.LeftArrow) && (this.transform.position.x <= 24.6)) {
			if (this.transform.position.x > 10.5) {
				this.transform.Translate(new Vector3 (-.1f, 0f, 0f));
			}
		}
	}
}
