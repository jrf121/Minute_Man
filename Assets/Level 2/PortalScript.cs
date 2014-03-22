using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	private GameObject player;
	private Vector2 initialStart;
	
	public Vector2 velocity;

	void Start () {
		player = GameObject.Find ("Minute Man");
		initialStart = this.gameObject.transform.GetChild(1).transform.position;
	}

	void OnTriggerEnter2D () {
		player.GetComponent<Transform>().position = new Vector3(initialStart.x, initialStart.y, -1f);
		player.rigidbody2D.velocity = velocity;
	}

	void Update () {
		velocity = player.rigidbody2D.velocity;
	}
}
