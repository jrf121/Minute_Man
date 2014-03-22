using UnityEngine;
using System.Collections;

public class TarmplineTarmpling : MonoBehaviour {
	public float bounciness;
	
	public float apex;
	
	private float maxYVelocityOnBounce;
	
	private GameObject man;
	
	void Start() {
		man = GameObject.Find ("Minute Man");
	}
	
	void FixedUpdate(){
		if (man.transform.position.x > this.transform.position.x - 10 && man.transform.position.x < this.transform.position.x + 10) {
			man.transform.parent = this.transform;
		}
		if (man.transform.position.x < this.transform.position.x - 10 || man.transform.position.x > this.transform.position.x + 10) {
			man.transform.parent = null;
		}
		
		if (man.GetComponent<PlayerController>().getPrevNow ().x >= 0 && man.GetComponent<PlayerController>().getPrevNow ().y <= 0){
				apex = man.transform.localPosition.y;
			}
		if (man.GetComponent<PlayerController>().onGround){
			apex = man.transform.localPosition.y;
		}
	}
		

	void OnTriggerEnter2D(Collider2D col){
		if (GameObject.Find ("Minute Man").Equals (col.gameObject)) {
			col.gameObject.rigidbody2D.AddForce(new Vector2(0f, bounciness*apex));
			
		}
	}
	
	public float getApex () {
		return apex;
	}
	public void setApex (float apex) {
		this.apex = apex;
	}
}
