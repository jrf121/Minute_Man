using UnityEngine;
using System.Collections;

public class Rotato2Script : MonoBehaviour {

	void Update () {
		this.transform.Rotate(new Vector3(0f, 0f, -1f));
	}
}
