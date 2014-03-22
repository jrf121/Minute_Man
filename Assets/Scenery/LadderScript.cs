using UnityEngine;
using System.Collections.Generic;

public class LadderScript : MonoBehaviour {

	public GameObject player;
	public bool onLadder;

	public Sprite[] climbing;
	public Queue<Sprite> spriteQueue;
	public int queueTimer;

	void Start () {
		spriteQueue = new Queue<Sprite>(climbing);
		queueTimer = 0;
	}

	void OnTriggerEnter2D () {
		onLadder = true;
		player = GameObject.Find("Minute Man");
		player.GetComponent<PlayerController>().ladder = this.gameObject;
	}
	void OnTriggerExit2D () {
		onLadder = false;
		player.GetComponent<PlayerController>().onGround = false;
		player.rigidbody2D.gravityScale = 5f;
		player.GetComponent<PlayerController>().spriteQueueFL = new Queue<Sprite>(player.GetComponent<PlayerController>().fallingLeft);
		player.GetComponent<PlayerController>().spriteQueueFR = new Queue<Sprite>(player.GetComponent<PlayerController>().fallingRight);
		
	}

	void Update () {
		if (Input.GetKey(KeyCode.UpArrow) && onLadder) {
			bool up = true;
			player.rigidbody2D.gravityScale = 0;
			player.GetComponent<PlayerController>().onGround = false;
			queueTimer++;
			if (queueTimer > 3) {
				queueTimer = 0;
				Sprite temp = spriteQueue.Dequeue();
				player.GetComponent<SpriteRenderer>().sprite = temp;
				spriteQueue.Enqueue(temp);
			}
			player.GetComponent<PlayerController>().climbLadder(this.gameObject, up);
		}
		
		else if (Input.GetKey(KeyCode.DownArrow) && onLadder) {
			bool up = false;
			player.rigidbody2D.gravityScale = 0;
			player.GetComponent<PlayerController>().onGround = false;
			queueTimer++;
			if (queueTimer > 3) {
				queueTimer = 0;
				Sprite temp = spriteQueue.Dequeue();
				player.GetComponent<SpriteRenderer>().sprite = temp;
				spriteQueue.Enqueue(temp);
			}
			player.GetComponent<PlayerController>().climbLadder(this.gameObject, up);
		}

		else if (onLadder) {
			player.GetComponent<PlayerController>().onGround = true;
		}
	}
}
