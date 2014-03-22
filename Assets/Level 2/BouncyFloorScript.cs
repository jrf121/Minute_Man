using UnityEngine;
using System.Collections;

public class BouncyFloorScript : MonoBehaviour {

	private GameObject man;
	
	void Start() {
		man = GameObject.Find ("Minute Man");
	}
	
	void OnCollisionEnter2D () {
		if (man.transform.position.y > this.transform.position.y) {
			man.rigidbody2D.AddForce (new Vector2(0f, 1600f));
		}
	}
}
