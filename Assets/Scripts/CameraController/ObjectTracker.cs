/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Tracker that allows following a local GameObject
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;


public class ObjectTracker : ITracker
{

    Vector3 LastPosition;
    Quaternion LastOrientation;

    //not sure if update is the best place. Consider UpdateLate
    void Update()
    {
        //if position changes, send update
        if(transform.position != LastPosition || transform.rotation != LastOrientation)
        {
            LastPosition = transform.position;
            LastOrientation = transform.rotation;

            TriggerPositionUpdate(transform.position, transform.rotation);
        }
    }
	void OnEnable()
	{
		//add tracker to tracker list
		ControllerHandler.AddTracker(this);
	}
	void OnDisable()
	{
		//remove tracker from tracker list
		ControllerHandler.RemoveTracker(this);
	}
    
	override public bool GetPosition(out Vector3 pos)
	{
		pos = transform.position;
		return true;
	}
	override public bool GetRotation(out Quaternion rot)
	{
		rot = transform.rotation;
		return true;
	}
	
	override public bool GetTransform(out Vector3 pos, out Quaternion rot)
	{
		pos = transform.position;
		rot = transform.rotation;
		return true;
	}
}
