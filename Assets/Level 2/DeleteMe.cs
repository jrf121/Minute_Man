using UnityEngine;
using System.Collections;

public class DeleteMe : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D col){
		this.rigidbody2D.AddForce(-(this.transform.position - col.transform.position).normalized * 10000);
	}
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
