using UnityEngine;
using System.Collections;

public class ButtonLvl1 : MonoBehaviour {

	public bool inRange;
	public GameObject platformRiser1;
	//public GameObject platformRiser2;

	void Start () {
		platformRiser1 = GameObject.Find ("PlatformRiser1");
		//platformRiser2 = GameObject.Find ("PlatformRiser2");
	}

	void OnTriggerStay2D () {
		inRange = true;
	}
	void OnTriggerExit2D () {
		inRange = false;
	}

	void Update () {
		if (inRange && Input.GetKey(KeyCode.Z)) {
			platformRiser1.GetComponent<RiserScript>().extend();
			//platformRiser2.GetComponent<RiserScript>().extend();
		}
	}
}
