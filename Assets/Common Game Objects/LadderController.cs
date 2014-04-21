using UnityEngine;
using System.Collections;

public class LadderController : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("Player"))
			col.gameObject.GetComponent<PlayerController>().validLadder.Add(this.gameObject);
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.CompareTag("Player"))
			col.gameObject.GetComponent<PlayerController>().validLadder.Remove(this.gameObject);
	}
		
}
