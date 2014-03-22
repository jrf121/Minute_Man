using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour {

	public GameObject player;

	void Start () {
		player = GameObject.Find("Minute Man");
	}
	
	void OnTriggerEnter2D () {
		if (player.GetComponent<PlayerController>().facing) {
			player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerController>().fallingRight[7];
		} else {
			player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerController>().fallingLeft[7];
		}
	}

	void OnTriggerStay2D () {
		player.GetComponent<PlayerController>().onGround = true;
	}
	void OnTriggerExit2D () {
		player.GetComponent<PlayerController>().onGround = false;
	}	
}
