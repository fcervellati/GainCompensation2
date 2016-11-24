/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * IFollower implementation that sets the camera position to the received update. 
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace HMD
{
    //Receives the new desired position from an ITracker and positions the GameObject
	public class MovementController : IFollower {

        [SerializeField]
        ITracker Tracker;
       
        public ITracker GetTracker()
        {
            return Tracker;
        }

		override public void SetTracker(ITracker t)
		{
			if(t.gameObject!=this.gameObject) //can't track itself
			{
				Debug.Log ("set tracker: " + t.name);
                if (Tracker != null)
                {
                    Tracker.UnsubscribeToPositionUpdate(SetPosition);
                }
                Tracker = t;
                Tracker.SubscribeToPositionUpdate(SetPosition);
				
			}
			else
			{
				Debug.LogWarning("Trying to track myself");
			}
		}
        
        virtual public void SetPosition(Vector3 pos, Quaternion ori)
        {
            transform.position = pos;
            transform.rotation = ori;
        }

		void OnDestroy()
		{
            if (Tracker != null)
                Tracker.UnsubscribeToPositionUpdate(SetPosition);
		}
		public void UnsubscribeToTracker()
		{
			if (Tracker != null) {
				Tracker.UnsubscribeToPositionUpdate(SetPosition);
			}
		}
		public void SubscribeToTracker()
		{
			if (Tracker != null) {
				Tracker.SubscribeToPositionUpdate(SetPosition);
			}
		}
		
	}

}