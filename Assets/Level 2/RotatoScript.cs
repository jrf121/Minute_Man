using UnityEngine;
using System.Collections;

public class RotatoScript : MonoBehaviour {

	void Update () {
		this.transform.Rotate(new Vector3(0f, 0f, 1f));
	}
}
