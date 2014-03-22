using UnityEngine;
using System.Collections.Generic;

public class PlatformScript : MonoBehaviour {

	public GameObject player;
	public bool touching;

	void Start () {
		player = GameObject.Find ("Minute Man");
	}

	void OnTriggerStay2D () {
		//player.GetComponent<PlayerController>().onGround = true;
	}
	void OnTriggerExit2D () {
		//player.GetComponent<PlayerController>().onGround = false;
	}
	
	void Update () {

		if (player.transform.position.y > this.transform.position.y) {
			this.collider2D.isTrigger = false;
		}
		if (player.transform.position.y <= this.transform.position.y) {
			this.collider2D.isTrigger = true;
		}

		if ((player.transform.position.y > this.transform.position.y) && (player.transform.position.y < this.transform.position.y + 2) && (player.transform.position.x > this.transform.position.x - (this.transform.localScale.x / 2)) && (player.transform.position.x < this.transform.position.x + (this.transform.localScale.x / 2))) {
			touching = true;
		}
		if (!(player.transform.position.y > this.transform.position.y) || !(player.transform.position.y < this.transform.position.y + 2) || !(player.transform.position.x > this.transform.position.x - (this.transform.localScale.x / 2)) || !(player.transform.position.x < this.transform.position.x + (this.transform.localScale.x / 2))) {
			touching = false;
		}

		if (Input.GetKey(KeyCode.DownArrow) && !this.collider2D.isTrigger && touching) {
			this.collider2D.isTrigger = true;
			player.GetComponent<PlayerController>().onGround = false;
			player.transform.Translate(0f, -.5f, 0f);
			touching = false;
		}
	}
}
