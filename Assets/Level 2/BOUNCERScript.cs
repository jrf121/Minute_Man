using UnityEngine;
using System.Collections;

public class BOUNCERScript : MonoBehaviour {
	
	private GameObject man;
	
	void Start() {
		man = GameObject.Find ("Minute Man");
	}
	
	void OnCollisionEnter2D () {
		man.rigidbody2D.AddForce( new Vector2((man.transform.position.x - this.transform.position.x) * 150f, (man.transform.position.y - this.transform.position.y) * 150f));
	}
}
