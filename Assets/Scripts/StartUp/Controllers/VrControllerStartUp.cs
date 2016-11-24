using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{

	public class VrControllerStartUp : StartUpController {


		override protected ITracker adaptController(GameObject go)
		{
            if (trackerName.Length == 0)
			    trackerName = "Oculus Controller";
			//if(OVRDevice.IsHMDPresent())
			{
				VrController oc = go.AddComponent<VrController>();
				go.name = "oculus Controller ";

				if(oc!=null)
				{
					return oc;
				}
				else
				{
					return null;
				}
			}
			/*else
			{
				return null;
			}*/

		}
	}

}