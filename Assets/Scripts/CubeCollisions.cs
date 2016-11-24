using UnityEngine;
using System.Collections;

public class CubeCollisions : MonoBehaviour {
	public GameObject Camera;
	public CameraController camScript;


	void Start () {
		camScript = Camera.GetComponent<CameraController>();
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "Elevator")													// to avoid falling through the elevator
			Camera.transform.SetParent (col.transform, true);
		if (col.gameObject.tag ==  "Target")										//to detect which ingredient are we close to
			camScript.zone = col.name;
	}

	void OnTriggerExit(Collider col) {
		if (col.name == "Elevator") {
			Camera.transform.SetParent (null, true);
			camScript.changeGain = true;
		}
		if (col.gameObject.tag ==  "Target")
			camScript.zone = null;
	}
}
