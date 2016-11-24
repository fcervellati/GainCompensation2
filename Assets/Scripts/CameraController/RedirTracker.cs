using UnityEngine;
using System.Collections;

abstract public class RedirTracker: ITracker
{
	protected Vector3 followerPosition; // in this case, camera
	protected Quaternion followerRotation;

	// declare an event different from base class event! in order to implement an override SubscribeToPositionUpdate function
	event PositionUpdate OnPositionUpdate;

	public ITracker realTracker; // real Tracker that this tracker subscribe to
	protected MonoBehaviour currentSubscriber;

//	void Awake()
//	{
//		ControllerHandler.AddTracker(this);
//	}
//	void OnDestroy()
//	{
//		ControllerHandler.RemoveTracker(this);
//	}

	void OnEnable()
	{
		//TODO: make sure the position of this tracker is set the same as the real tracker
		// start tracking the tracker
		if (realTracker != null) {
			//Debug.Log ("subscribed to real tracker");
			// set the initial position of the tracker to be the same as the tracker it is following
			Vector3 pos;
			Quaternion ori;
			realTracker.GetPosition (out pos);
			realTracker.GetRotation (out ori);
			SetInitialPosition (pos, ori);
			realTracker.SubscribeToPositionUpdate (CalculateRedirPosition);
		}
	}

	abstract protected void CalculateRedirPosition(Vector3 pos, Quaternion ori);
	override protected void TriggerPositionUpdate(Vector3 pos, Quaternion ori)
	{
		// Make a temporary copy of the event to avoid possibility of
		// a race condition if the last subscribesr unsubscribes
		// immediately after the null check and before the event is raised.
		PositionUpdate handler = OnPositionUpdate;
		if (handler != null)
		{
			handler(pos, ori);
		}
	}
	override public void SubscribeToPositionUpdate(PositionUpdate positionUpdateFunction)
	{
		// SINGLE SUBSCRIBER
		// If there is already subscriber, unsubscribe them all, and subscribe the new one 
		if (OnPositionUpdate != null && OnPositionUpdate.GetInvocationList ().Length> 0) {
//			Debug.Log ("There is already a subscriber");
			OnPositionUpdate = null;
		}
		if (currentSubscriber != null) {
//			Debug.Log ("Reset current subscriber");	
			currentSubscriber = null;
		}
		OnPositionUpdate += positionUpdateFunction;
		currentSubscriber = positionUpdateFunction.Target as MonoBehaviour;

		// added on 04 May 2016
//		currentSubscriber.transform.position = m_position;
//		var anglex = transform.rotation.eulerAngles.x;
//		var angley = currentSubscriber.transform.rotation.eulerAngles.y;
//		var anglez = transform.rotation.eulerAngles.z;
//		currentSubscriber.transform.rotation = Quaternion.Euler (anglex, angley, anglez);

//		followerPosition = currentSubscriber.transform.position;
//		followerRotation = currentSubscriber.transform.rotation;
	}
	override public void UnsubscribeToPositionUpdate(PositionUpdate fnc)
	{
		//unsubscribe
		OnPositionUpdate -= fnc;
	}
//	//if enabled get current position, if disabled give target position or return false if no target exists
//	override public bool GetPosition(out Vector3 pos)
//	{
//		if(enabled)
//		{
//			pos = transform.position;
//			return true;
//		}
//		else
//		{
//			pos = Vector3.zero;
//			return false;
//		}
//	}
//	override public bool GetRotation(out Quaternion rot)
//	{
//		if(enabled)
//		{
//			rot = transform.rotation;
//			return true;
//		}
//		else
//		{
//			rot = new Quaternion();
//			return false;
//		}
//	}
//
//	override public bool GetTransform(out Vector3 pos, out Quaternion rot)
//	{
//		if(enabled)
//		{
//			pos = transform.position;
//			rot = transform.rotation;
//			return true;
//		}
//		else
//		{   
//			pos = Vector3.zero;
//			rot = new Quaternion();
//			return false;
//		}
//
//	}
//
}


