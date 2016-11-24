using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{

	public class KeyboardControllerStartUp : StartUpController {

		override protected ITracker adaptController(GameObject go)
		{
            if (trackerName.Length == 0)
                trackerName = "keyboard Controller";

            go.name = trackerName;

			return go.AddComponent<KeyboardController>();

		}
	}

}