using UnityEngine;
using System.Collections;

public class OtherBouncerMoverScript : MonoBehaviour {

	private bool movinL = true;
	

	void Update () {
		if (movinL) {
			this.transform.Translate (-.1f, 0f, 0f);
		}
		if (this.transform.position.x < 27.5) {
			movinL = false;
		}
		
		if (!movinL) {
			this.transform.Translate (.1f, 0f, 0f);
		}
		if (this.transform.position.x > 30) {
			movinL = true;
		}
	}
}
