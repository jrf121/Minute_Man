using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	float prev;
	float now;

	Vector2 jump = new Vector2 (0f, 900f);
	Vector2 move = new Vector2 (40f, 0f);
	Vector2 moveInAir = new Vector2 (20f, 0f);
	Vector2 climb = new Vector3 (0f, .20f, 0f);
	public Vector2 drag;
	public bool facing = true;

	public Sprite left;
	public Sprite right;

	public Sprite[] runningRight;
	public Sprite[] runningLeft;
	public Sprite[] fallingRight;
	public Sprite[] fallingLeft;

	public Queue<Sprite> spriteQueueR;
	public Queue<Sprite> spriteQueueL;
	public Queue<Sprite> spriteQueueFR;
	public Queue<Sprite> spriteQueueFL;

	public int queueTimerR;
	public int queueTimerL;
	public int queueTimerFR;
	public int queueTimerFL;
	
	public GameObject ladder;

	public bool onGround;
	public float dragScale = 5;

	void Start () {
		ladder = GameObject.Find ("Ladder3Prefab");
		spriteQueueR = new Queue<Sprite>(runningRight);
		spriteQueueL = new Queue<Sprite>(runningLeft);
		spriteQueueFR = new Queue<Sprite>(fallingRight);
		spriteQueueFL = new Queue<Sprite>(fallingLeft);
		queueTimerR = 0;
		queueTimerL = 0;
		queueTimerFR = 0;
		queueTimerFL = 0;
		prev = 0f;
		now = 0f;
	}

	void Update () {
		
		// Check line 182 for code
		this.addDrag();
		
		// Housed at line 187
		if (Input.GetKeyDown(KeyCode.Space) && onGround) {
			this.jumpM();
		}
		// Housed at line 202
		if (Input.GetKey(KeyCode.RightArrow) && onGround) {
			this.runRight();
		}
		// Housed at line 217
		if (Input.GetKey(KeyCode.LeftArrow) && onGround) {
			this.runLeft();
		}
		// Housed at line 232
		if (Input.GetKey(KeyCode.RightArrow) && !onGround) {
			this.fallRight();
		}
		// Housed at line 238
		if (Input.GetKey(KeyCode.LeftArrow) && !onGround) {
			this.fallLeft();
		}
		// Housed at line 244
		if (Input.GetKeyUp(KeyCode.RightArrow) && onGround) {
			this.faceRight();
		}
		// Housed at line 249
		if (Input.GetKeyUp(KeyCode.LeftArrow) && onGround) {
			this.faceLeft();
		}
// THIS IS THE FALLING ANIMATION JUST SO YOU KNOOOOOOWWWWWWW
// And I don't even want to try to make this into a helper method of some kind...
// TO DO: PLEASE FIGURE OUT HOW TO MAKE ANIMATIONS WORK IN UNITY. NOT VIA MY SILLIES.
		if (!onGround && !ladder.GetComponent<LadderScript>().onLadder) {
			if (facing) {
				if (this.rigidbody2D.velocity.y > 0) {
					queueTimerFL = 0;
					spriteQueueFL = new Queue<Sprite>(fallingLeft);
					queueTimerFR++;
					if (queueTimerFR > 4) {
						if (spriteQueueFR.Peek().Equals(fallingRight[3])) {
							this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteQueueFR.Peek();
							queueTimerFR = 0;
						}
						else {
							Sprite temp = spriteQueueFR.Dequeue();
							this.gameObject.GetComponent<SpriteRenderer>().sprite = temp;
							spriteQueueFR.Enqueue(temp);
							queueTimerFR = 0;
						}
					}
				}
				if (this.rigidbody2D.velocity.y < 0) {
					queueTimerFR++;
					if (queueTimerFR > 5) {
						if (spriteQueueFR.Peek().Equals(fallingRight[6])) {
							this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteQueueFR.Peek();	
							queueTimerFR = 0;
						}
						else {
							Sprite temp = spriteQueueFR.Dequeue();
							this.gameObject.GetComponent<SpriteRenderer>().sprite = temp;
							spriteQueueFR.Enqueue(temp);
							queueTimerFR = 0;
						}
					}
				}
			}
		
		
			if (!facing) {
				if (this.rigidbody2D.velocity.y > 0) {
					queueTimerFR = 0;
					spriteQueueFR = new Queue<Sprite>(fallingRight);
					queueTimerFL++;
					if (queueTimerFL > 4) {
						if (spriteQueueFL.Peek().Equals(fallingLeft[3])) {
							this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteQueueFL.Peek();
							queueTimerFL = 0;
						}
						else {
							Sprite temp = spriteQueueFL.Dequeue();
							this.gameObject.GetComponent<SpriteRenderer>().sprite = temp;
							spriteQueueFL.Enqueue(temp);
							queueTimerFL = 0;
						}
					}
				}
				if (this.rigidbody2D.velocity.y < 0) {
					queueTimerFL++;
					if (queueTimerFL > 5) {
						if (spriteQueueFL.Peek().Equals(fallingLeft[6])) {
							this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteQueueFL.Peek();	
							queueTimerFL = 0;
						}
						else {
							Sprite temp = spriteQueueFL.Dequeue();
							this.gameObject.GetComponent<SpriteRenderer>().sprite = temp;
							spriteQueueFL.Enqueue(temp);
							queueTimerFL = 0;
						}
					}
				}
			}
		}
		
		if (onGround && facing) {
			for (int i = 0; i < fallingRight.Length; i++) {
				if (this.gameObject.GetComponent<SpriteRenderer>().sprite.Equals(fallingRight[i])) {
					this.gameObject.GetComponent<SpriteRenderer>().sprite = right;
					spriteQueueFR = new Queue<Sprite>(fallingRight);
				}
			}
		}
		if (onGround && !facing) {
			for (int i = 0; i < fallingLeft.Length; i++) {
				if (this.gameObject.GetComponent<SpriteRenderer>().sprite.Equals(fallingLeft[i])) {
					this.gameObject.GetComponent<SpriteRenderer>().sprite = left;
					spriteQueueFL = new Queue<Sprite>(fallingLeft);
				}
			}
		}
		
		this.checkOnGround();
	}
	
	private void addDrag() {
		drag = new Vector2 (-this.rigidbody2D.velocity.x, -this.rigidbody2D.velocity.y);
		this.rigidbody2D.AddForce (dragScale * drag);
	}
	
	private void jumpM() {
		if (facing) {
			spriteQueueFR = new Queue<Sprite>(fallingRight);
		} else {
			spriteQueueFL = new Queue<Sprite>(fallingLeft);
		}
		if (this.rigidbody2D.gravityScale == 0) {
			this.rigidbody2D.AddForce(new Vector2 (0f, 500f));
		}
		else {
			this.rigidbody2D.AddForce (jump);
		}
		//onGround = false;
	}
	
	private void runRight() {
		queueTimerR++;
		queueTimerL = 0;
		if (queueTimerR > 2) {
			Sprite temp = spriteQueueR.Dequeue();		
			this.gameObject.GetComponent<SpriteRenderer>().sprite = temp;
			spriteQueueR.Enqueue(temp);
			queueTimerR = 0;
		}
		if (this.rigidbody2D.velocity.x < 12f) {
			this.rigidbody2D.AddForce (move);
		}
		facing = true;
	}
	
	private void runLeft() {
		queueTimerL++;
		queueTimerR = 0;
		if (queueTimerL > 2) {
			Sprite temp = spriteQueueL.Dequeue();		
			this.gameObject.GetComponent<SpriteRenderer>().sprite = temp;
			spriteQueueL.Enqueue(temp);
			queueTimerL = 0;
		}
		if (this.rigidbody2D.velocity.x > -12f) {
			this.rigidbody2D.AddForce (-move);
		}
		facing = false;
	}
	
	private void fallRight() {
		if (this.rigidbody2D.velocity.x < 15f) {
			this.rigidbody2D.AddForce (moveInAir);
		}
	}
	
	private void fallLeft() {
		if (this.rigidbody2D.velocity.x > -15f) {
			this.rigidbody2D.AddForce (-moveInAir);
		}
	}
	
	private void faceRight() {
		facing = true;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = right;
	}
	
	private void faceLeft() {
		facing = false;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = left;
	}
	
	private void checkOnGround() {
		prev = now;
		now = this.rigidbody2D.velocity.y;
		
		if ((prev == 0f) && (now == 0f)) 
			this.onGround = true;
		else
			this.onGround = false;
	}

	public Vector2 getPrevNow() {
		return new Vector2(prev, now);
	}

	public void climbLadder (GameObject ladder, bool up) {

		//this.onGround = true;
		this.transform.position = new Vector3 (ladder.transform.position.x, this.transform.position.y, this.transform.position.z);
		this.rigidbody2D.velocity = new Vector2 (0f, 0f);

		if (up) {
			this.transform.Translate(climb);
		}
		if (!up) {
			this.transform.Translate(-climb);
		}
	}

	public void climbStairsL (GameObject stair, bool lr) {
		
		this.rigidbody2D.velocity = new Vector2 (0f, 0f);
		
		if (lr) {
			this.transform.Translate (new Vector3 (.1f, -.12f, 0f));
		}
		if (!lr) {
			this.transform.Translate (new Vector3 (-.1f, .1f, 0f));
		}
	}

	public void climbStairsR (GameObject stair, bool lr) {
		
		this.rigidbody2D.velocity = new Vector2 (0f, 0f);
		
		if (lr) {
			this.transform.Translate (new Vector3 (.1f, .1f, 0f));
		}
		if (!lr) {
			this.transform.Translate (new Vector3 (-.1f, -.12f, 0f));
		}
	}
	

}
