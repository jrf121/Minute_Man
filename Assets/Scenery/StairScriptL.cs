using UnityEngine;
using System.Collections;

public class StairScriptL : MonoBehaviour {

	public GameObject player;
	public bool onStairs;
	public bool lr;

	void Start () {
		player = GameObject.Find("Minute Man");
	}
	
	void OnTriggerStay2D () {
		onStairs = true;
		player.GetComponent<PlayerController>().onGround = true;
		player.rigidbody2D.gravityScale = 0;
	}
	void OnTriggerExit2D () {
		onStairs = false;
		player.GetComponent<PlayerController>().onGround = false;
		player.rigidbody2D.gravityScale = 5;
	}

	void Update () {
		if (onStairs) {
			player.GetComponent<PlayerController>().onGround = true;
		}
		
		if (onStairs && Input.GetKey(KeyCode.RightArrow)) {
			lr = true;
			player.GetComponent<PlayerController>().climbStairsL(this.gameObject, lr);
		}
		if (onStairs && Input.GetKey(KeyCode.LeftArrow)) {
			lr = false;
			player.GetComponent<PlayerController>().climbStairsL(this.gameObject, lr);
		}
	}
}
