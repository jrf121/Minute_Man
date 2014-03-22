using UnityEngine;
using System.Collections;

public class ThatOneOtherBouncerMoverScriptThing : MonoBehaviour {

	private bool movinR = true;
	
	
	void Update () {
		if (movinR) {
			this.transform.Translate (.1f, 0f, 0f);
		}
		if (this.transform.position.x > 22.5) {
			movinR = false;
		}
		
		if (!movinR) {
			this.transform.Translate (-.1f, 0f, 0f);
		}
		if (this.transform.position.x < 20) {
			movinR = true;
		}
	}
}
