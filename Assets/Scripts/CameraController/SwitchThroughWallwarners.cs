/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Switches the real position tracker of the assigned WallWarningController
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public class SwitchThroughWallwarners : MonoBehaviour {

	public WallWarningController Controller;

	public void SwitchControllers()
	{

        //get list of trackers
		List<ITracker> trackers = ControllerHandler.GetTrackers();

        ITracker newTracker = null;
	
        //find the first on after the current tracker
        int currentIndex = -1;
		for(int i = 0;i<trackers.Count;i++)
		{
            if (Controller.GetRealPositionTracker() == trackers[i])
			{
                currentIndex = i;
                break;
            }
        }

        newTracker = trackers[(currentIndex + 1) % trackers.Count];
        
		if(newTracker!=null)
		{
            //show message
            MessageSystem.Line.Main.AddMessage(new MessageSystem.Message("" + Controller.name + " set to " + newTracker.name, 0.5f));
            //set new tracker
			Controller.SetRealPositionTracker(newTracker);
		}

	}
}
