using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

namespace StartUp
{
	public class AssignTrackerTo : StartUpScript {

        public ITracker tracker;
        public IFollower target;

		override public void StartUp()
		{
			target.SetTracker(tracker);
		

		}
	}
}