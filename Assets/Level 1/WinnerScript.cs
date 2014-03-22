using UnityEngine;
using System.Collections;

public class WinnerScript : MonoBehaviour {

	void OnTriggerEnter2D () {
		this.gameObject.GetComponent<MeshRenderer>().enabled = true;
	}
}
