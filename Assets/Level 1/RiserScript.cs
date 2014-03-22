using UnityEngine;
using System.Collections;

public class RiserScript : MonoBehaviour {

	public GameObject player;

	void Start () {
		player = GameObject.Find("Minute Man");
	}

	public void extend () {
		while (this.transform.position.x < -0.5f) {
			this.transform.Translate (0.1f, 0f, 0f);
		}
	}

	/*void Update () {
		if ((player.transform.position.x < this.transform.position.x + 4) && (player.transform.position.x > this.transform.position.x - 4) && (player.transform.position.y > this.transform.position.y)) {
			if (this.transform.position.y < 12f) {
				this.transform.Translate (new Vector3 (0f, .01f, 0f));
			}
		}
		if ((player.transform.position.x > this.transform.position.x + 4) || (player.transform.position.x < this.transform.position.x - 4) || (player.transform.position.y < this.transform.position.y)) {
			if (this.transform.position.y > 9.5f) {
				this.transform.Translate (new Vector3 (0f, -.01f, 0f));
			}
		}
	}*/
}
