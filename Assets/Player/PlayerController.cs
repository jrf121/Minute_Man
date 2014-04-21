using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float speedCap;
	public float inAirMovementReductionFactor;
	public float jumpSpeed;
	public float climbSpeed;
	public float drag;
	public float slidiness;
	public float slideAngle;
	public List<GameObject> validLadder;
	
	private Animator animator;
	private float friciton;
	private bool onGround;
	private bool onLadder = false;
	private float gravityScale;


	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		gravityScale = this.rigidbody2D.gravityScale;
	}
	
	
	
	void Update(){
		if (validLadder.Count > 0 && Input.GetAxis("Vertical") != 0){
			onLadder = true;
			this.rigidbody2D.velocity = Vector2.zero;
			this.rigidbody2D.gravityScale = 0f;
			this.transform.position = new Vector3(validLadder[validLadder.Count-1].transform.position.x, this.transform.position.y, this.transform.position.z);
		}
		if (onLadder){

			this.transform.position += new Vector3(0f, Input.GetAxis("Vertical") * climbSpeed, 0f);
			if (Input.GetAxis("Horizontal") != 0 || Input.GetButtonDown("Jump") || validLadder.Count == 0){
				onLadder = false;
				this.rigidbody2D.gravityScale = gravityScale;
				if (Input.GetButtonDown("Jump"))
					this.rigidbody2D.velocity += new Vector2 (0f, jumpSpeed);
			}
		}
		
		onGround = isOnGround();
		
		int xDir = (int)(Mathf.Abs(this.rigidbody2D.velocity.x) <= 0.1 ? 0f : 2f*this.rigidbody2D.velocity.x/Mathf.Abs(this.rigidbody2D.velocity.x));
		int yDir = (int)(Mathf.Abs(this.rigidbody2D.velocity.y) <= 0.1 ? 0f : 2f*this.rigidbody2D.velocity.y/Mathf.Abs(this.rigidbody2D.velocity.y));
		animator.SetInteger("xVelocity", xDir);
		animator.SetInteger("yVelocity", yDir);
		animator.SetBool ("onGround", onGround);
		animator.SetBool ("onLadder", onLadder);
		if (onGround)
			this.rigidbody2D.AddForce(new Vector2(speedCap * Input.GetAxis("Horizontal"), 0f));
		else
			this.rigidbody2D.AddForce(new Vector2(speedCap * Input.GetAxis("Horizontal")/inAirMovementReductionFactor, 0f));
		if (Input.GetAxis("Horizontal") == 0)
			this.rigidbody2D.AddForce(new Vector2(-this.rigidbody2D.velocity.normalized.x * 1/slidiness * drag * Mathf.Pow (this.rigidbody2D.velocity.x, 2f), 0f));
		else
			this.rigidbody2D.AddForce(new Vector2(-this.rigidbody2D.velocity.normalized.x * drag * Mathf.Pow (this.rigidbody2D.velocity.x, 2f), 0f));
		if (Input.GetButtonDown("Jump") && onGround){
			this.rigidbody2D.velocity += new Vector2 (0f, jumpSpeed);
		}
	}
	
	
	
	bool isOnGround(){
		BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
		Vector2 leftFoot = (Vector2)this.transform.position + collider.center - collider.size/2f;
		Vector2 rightFoot = (Vector2)this.transform.position + collider.center + new Vector2 (collider.size.x, -collider.size.y)/2f;
		RaycastHit2D hitLeft = Physics2D.Raycast(leftFoot, Vector3.down) ;
		RaycastHit2D hitRight = Physics2D.Raycast(rightFoot, Vector3.down);
		
		if (hitLeft.collider != null && (hitLeft.point - leftFoot).magnitude <= 0.1f || hitRight.collider != null && (hitRight.point - rightFoot).magnitude <= 0.1f)
			return true;

		return false;	
	}
}
