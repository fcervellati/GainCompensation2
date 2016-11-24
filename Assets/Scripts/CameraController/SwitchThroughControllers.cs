/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Switches through the list on ITrackers for Controller
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public class SwitchThroughControllers : MonoBehaviour {

	public HMD.MovementController Controller;
	public event System.Action OnControllerSwitched;
	ITracker current;
//	public ITracker GetCurrentController()
//	{
//		return current;
//	}
//	public void SetCurrentController (ITracker t)
//	{
//		if (t != null)
//			Controller.SetTracker (t);
//	}
	public void SwitchControllers()
	{
		List<ITracker> trackers = ControllerHandler.GetTrackers();

        current = null;
	
        int me = -1;
		for(int i = 0;i<trackers.Count;i++)
		{
            if (Controller.GetTracker() == trackers[i])
			{
                me = i;
                break;
            }
        }
        for (int i = 1; i < trackers.Count; i++)
        {
            if (trackers[(me + i) % trackers.Count].gameObject != gameObject)
            {
                Debug.LogWarning("Reached this even if the compiler said we can't");
                current = trackers[(me + i) % trackers.Count];
                break;
            } 
		}

		if(current!=null)
		{
            MessageSystem.Line.Main.AddMessage(new MessageSystem.Message("" + Controller.name + " set to " + current.name, 0.5f));
			Controller.SetTracker(current);
		}
		ControllerSwitched ();

	}
	private void ControllerSwitched()
	{
		System.Action handler = OnControllerSwitched;
		if (handler != null)
			handler ();
	}
}
