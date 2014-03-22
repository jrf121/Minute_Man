using UnityEngine;
using System.Collections;

public class BouncerDicksScript : MonoBehaviour {

	private bool goinUp = true;
	

	void Update () {
		if (goinUp) {
			this.transform.Translate (new Vector3(0f, 0f, -.1f));
		}
		if (this.transform.position.y > -33f) {
			goinUp = false;
		}
		
		if (!goinUp) {
			this.transform.Translate (new Vector3(0f, 0f, .1f));
		}
		if (this.transform.position.y < -41f) {
			goinUp = true;
		}
	}
}
