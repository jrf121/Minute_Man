using UnityEngine;
using System.Collections;

public class ImGoingForIt : MonoBehaviour {
	public float movementSpeedCap;
	public float acceleration;
	
	private float time_directionFactor;
	private int xDir,yDir;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){
		Animator animator = this.GetComponent<Animator>();
		int xDir = (int)(this.rigidbody2D.velocity.x == 0 ? 0f : 2f*this.rigidbody2D.velocity.x/Mathf.Abs(this.rigidbody2D.velocity.x));
		int yDir = (int)(this.rigidbody2D.velocity.y == 0 ? 0f : 2f*this.rigidbody2D.velocity.y/Mathf.Abs(this.rigidbody2D.velocity.y));
		Debug.Log(xDir);
		Debug.Log (rigidbody2D.velocity);
		animator.SetInteger("xVelocity", xDir);
		animator.SetInteger("yVelocity", yDir);
	}
	
	void FixedUpdate () {
	xDir = (int)(this.rigidbody2D.velocity.x == 0 ? 0f : 2f*this.rigidbody2D.velocity.x/Mathf.Abs(this.rigidbody2D.velocity.x));
	yDir = (int)(this.rigidbody2D.velocity.y == 0 ? 0f : 2f*this.rigidbody2D.velocity.y/Mathf.Abs(this.rigidbody2D.velocity.y));
	if (Input.GetAxis("Horizontal") < 0)
		this.rigidbody2D.velocity = new Vector2(this.CalculateXVelocity(-1), this.rigidbody2D.velocity.y);
	if (Input.GetAxis("Horizontal") > 0)
		this.rigidbody2D.velocity = new Vector2(this.CalculateXVelocity(1), this.rigidbody2D.velocity.y);
	if (Input.GetAxis("Horizontal") == 0)
		this.rigidbody2D.velocity = new Vector2(this.CalculateXVelocity(0), this.rigidbody2D.velocity.y);
	}
	
	//Returns the proper velocity for Minute Man. Pass this method 1 for right, -1 for left, and 0 for neither
	float CalculateXVelocity(int direction){
		switch(direction){
			case -1:
				if (time_directionFactor >= 0)
					time_directionFactor = 0;
				time_directionFactor -= acceleration * Time.deltaTime;
				break;
			case 0:
				time_directionFactor = 0;
				Debug.Log("Shit is real!");
				break;
			case 1:
				if (time_directionFactor <= 0)
					time_directionFactor = 0;
				time_directionFactor += acceleration * Time.deltaTime;
				break;
			default: break;	
			}	
		
		return 2 * movementSpeedCap * (1/(1 + Mathf.Exp(-time_directionFactor)) - 0.5f);
		
	}
	
	
	void MoveRight(){
	}
		
}
