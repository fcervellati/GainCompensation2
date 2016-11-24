using UnityEngine;
using System.Collections;
using System;

public class TargetController : MonoBehaviour {
	public event Action OnTargetReached;
	public event Action OnTargetExit;
	public event Action OnTargetAligned;
	public event Action OnTargetReachedAndAligned;
	Collider currentCollider = null;
	bool targetReached;
	public bool TargetReachedFlag;
	bool targetAligned;
	float openingAngle = 1f;
	float lowerBound;
	float upperBound;
	float selfAngle;


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			TargetReached ();
			//Debug.Log ("trigger entered");
			targetReached = true;
			TargetReachedFlag = true;
			currentCollider = other;

		}
		
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) {
			//Debug.Log ("trigger exit");
			targetReached = false;
			TargetExit ();
		}
		currentCollider = null;

	}
	void TargetExit()
	{
		Action handler = OnTargetExit;
		if (handler != null) {
			handler ();
		}
	}
	void TargetReached()
	{
		Action handler = OnTargetReached;
		if (handler != null) {
			handler ();
		}
	}
	void TargetAligned()
	{
		Action handler = OnTargetAligned;
		if (handler != null) {
			handler ();
		}
	}
	void TargetReachedAndAligned()
	{
		Action handler = OnTargetReachedAndAligned;
		if (handler != null) {
			handler ();
		}
	}
	void Update()
	{
		if (currentCollider != null) {
			lowerBound = transform.rotation.eulerAngles.y - openingAngle;
			upperBound = transform.rotation.eulerAngles.y + openingAngle;
			selfAngle = currentCollider.transform.rotation.eulerAngles.y;
			if (selfAngle-lowerBound > 0f && upperBound-selfAngle > 0f) {
				if (targetReached == true) {
					TargetReachedAndAligned();
					targetReached = false;
				} else
					TargetAligned ();
				targetAligned = true;
			}
		}
	}
}
