/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * static class that handles the list of available trackers
 * 
***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerHandler {

	//adds standard trackers (keyboard/oculus/rewave) and habdles a static tracker list
	static List<ITracker> TrackerList;

	//get tracker list
	static public List<ITracker> GetTrackers()
	{
		return TrackerList;
	}

	//add a tracker to the list
	static public void AddTracker(ITracker add)
	{
		//if the list doesn't exist, create one
		if(TrackerList == null)
		{
			TrackerList = new List<ITracker>();
		}
		else
		{
			//if it exists try to remove the new tracker
			TrackerList.Remove(add);
		}

        //add the new tracker
        if (!TrackerList.Contains(add))
        {
            TrackerList.Add(add);
        }

	}
	static public void RemoveTracker(ITracker rem)
	{
		//if tracker list exists
		if(TrackerList!=null)
		{
			//remove tracker
			TrackerList.Remove(rem);
		}
	}

	
}
