using UnityEngine;
using System.Collections;

public class DingScript : MonoBehaviour {

	private bool sex;
	private int sexCounter = 0;
	private Material unDinged;
	
	public Material dinged;
	
	void Start () {
		unDinged = this.renderer.sharedMaterial;
	}

	void OnCollisionEnter2D () {
		sex = true;
		this.audio.Play();
	}
	
	void Update () {
		if (sex) {
			this.renderer.sharedMaterial = dinged;
			sexCounter++;
		}
		if (sexCounter > 6) {
			this.renderer.sharedMaterial = unDinged;
			sexCounter = 0;
			sex = false;
		}
	}
}
