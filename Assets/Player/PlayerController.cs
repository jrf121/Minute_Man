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
	private int inAirDirection;


	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		gravityScale = this.rigidbody2D.gravityScale;
	}
	
	
	
	void Update(){
	
		//Ladder climbing junk yeahhhhh
		if (validLadder.Count > 0 && Input.GetButtonDown("Vertical") && !onLadder){
			onLadder = true;
			this.rigidbody2D.velocity = Vector2.zero;
			this.rigidbody2D.gravityScale = 0f;
			animator.SetTrigger("onLadder");
		}
		
		if (onLadder){
			if (validLadder.Count > 0)
				this.transform.position = new Vector3(	validLadder[validLadder.Count-1].transform.position.x, 
			                                     		this.transform.position.y + Input.GetAxis("Vertical") * climbSpeed, 
			                                      		this.transform.position.z);
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Climbing"))
				animator.speed = Mathf.Abs(Input.GetAxis("Vertical")) * climbSpeed * 10f;
			if (Input.GetAxis("Horizontal") != 0 || Input.GetButtonDown("Jump") || validLadder.Count == 0){
				onLadder = false;
				this.rigidbody2D.gravityScale = gravityScale;
				animator.SetTrigger("offLadder");
				animator.speed = 1;
				if (Input.GetButtonDown("Jump"))
					this.rigidbody2D.velocity += new Vector2 (0f, jumpSpeed);
			}
		}
		
		onGround = isOnGround();
		if (!onGround)
			inAirDirection = (int)Mathf.Sign(this.rigidbody2D.velocity.x); 
		
		//These ints be used by the anamation controller for junk
		int xDir = (int)(Mathf.Abs(this.rigidbody2D.velocity.x) <= 0.1 ? 0f : Mathf.Sign (this.rigidbody2D.velocity.x));
		int yDir = (int)(Mathf.Abs(this.rigidbody2D.velocity.y) <= 0.1 ? 0f : Mathf.Sign (this.rigidbody2D.velocity.y));
		
		//There goes that junk to the anamation controller
		animator.SetInteger("xVelocity", xDir);
		animator.SetInteger("yVelocity", yDir);
		animator.SetBool ("onGround", onGround);
		
		//Makes Minute Man walk
		if (onGround || Input.GetAxis("Horizontal") == inAirDirection)
			this.rigidbody2D.AddForce(new Vector2(speedCap * Input.GetAxis("Horizontal"), 0f));
			
		//He moves slower in the air
		else
			this.rigidbody2D.AddForce(new Vector2(speedCap * Input.GetAxis("Horizontal")/inAirMovementReductionFactor, 0f));
		
		//He slides to a hault much faster than he otherwise would because of this
		if (Input.GetAxis("Horizontal") == 0)
			this.rigidbody2D.AddForce(new Vector2(-this.rigidbody2D.velocity.normalized.x * 1/slidiness * drag * Mathf.Pow (this.rigidbody2D.velocity.x, 2f), 0f));
		else
			this.rigidbody2D.AddForce(new Vector2(-this.rigidbody2D.velocity.normalized.x * drag * Mathf.Pow (this.rigidbody2D.velocity.x, 2f), 0f));
		
		//Lez jump
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
