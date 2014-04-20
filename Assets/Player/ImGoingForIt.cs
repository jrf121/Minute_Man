using UnityEngine;
using System.Collections;

public class ImGoingForIt : MonoBehaviour {
	public float speedCap;
	public float movementAcceleration;
	public float inAirMovementReductionFactor;
	public float jumpSpeed;
	
	private float timeParameter = 0;
	private bool onGround;


	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update(){
		Animator animator = this.GetComponent<Animator>();
		
		onGround = isOnGround();
		
		int xDir = (int)(this.rigidbody2D.velocity.x == 0 ? 0f : 2f*this.rigidbody2D.velocity.x/Mathf.Abs(this.rigidbody2D.velocity.x));
		int yDir = (int)(Mathf.Abs(this.rigidbody2D.velocity.y) <= 0.1 ? 0f : 2f*this.rigidbody2D.velocity.y/Mathf.Abs(this.rigidbody2D.velocity.y));
		animator.SetInteger("xVelocity", xDir);
		animator.SetInteger("yVelocity", yDir);
		animator.SetBool ("onGround", onGround);
		if (onGround)
			this.rigidbody2D.velocity = FindVelocity(speedCap, movementAcceleration);
		else
			this.rigidbody2D.velocity = FindVelocity(speedCap/inAirMovementReductionFactor, movementAcceleration/inAirMovementReductionFactor);
	}	
	
	Vector2 FindVelocity(float movementSpeedCap, float acceleration){
		if (Input.GetButtonDown("Jump") && onGround){
			this.rigidbody2D.velocity += new Vector2 (0f, jumpSpeed);
		}
		
		switch((int) Input.GetAxis("Horizontal")){
		case -1:
			timeParameter = timeParameter > 0 ? 0 : timeParameter - acceleration * Time.deltaTime;
			break;
		case 0: 
			timeParameter = 0;
			break;
		case 1:
			timeParameter = timeParameter < 0 ? 0 : timeParameter + acceleration * Time.deltaTime;
			break;
		}
		return new Vector2(movementSpeedCap * (1 / (1+Mathf.Exp(-timeParameter)) - .5f), this.rigidbody2D.velocity.y);
		
	}
	
	bool isOnGround(){
		BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
		Vector2 leftFoot = (Vector2)this.transform.position + collider.center - collider.size/2f;
		Vector2 rightFoot = (Vector2)this.transform.position + collider.center + new Vector2 (collider.size.x, -collider.size.y)/2f;
		RaycastHit2D hitLeft = Physics2D.Raycast(leftFoot, Vector3.down, .05f) ;
		RaycastHit2D hitRight = Physics2D.Raycast(rightFoot, Vector3.down, .05f);
		
		if (hitLeft.collider != null || hitRight.collider != null)
			return true;

		return false;	
	}
}
