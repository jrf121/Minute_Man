using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour {

	private bool inRange;
	public GameObject elevator;
	//public GameObject platformRiser2;
	
	void Start () {
	}
	
	void OnTriggerStay2D () {
		inRange = true;
	}
	void OnTriggerExit2D () {
		inRange = false;
	}
	
	void Update () {
		if (inRange && Input.GetKeyDown(KeyCode.UpArrow)) {
			GameObject.Find("Minute Man").transform.position=new Vector3(elevator.transform.position.x,elevator.transform.position.y,-1);
			//platformRiser2.GetComponent<RiserScript>().extend();
		}
	}
}
