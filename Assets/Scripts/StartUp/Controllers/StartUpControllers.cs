using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{

	abstract public class StartUpController : StartUpScript {

        public GameObject trackerPrefab;

		override public void StartUp()
		{
			status = true;
			createController();
		}


		GameObject init()
		{
			/*if(Network.isServer || Network.isClient)
			{
				return Network.Instantiate(go,Vector3.zero,Quaternion.identity,0) as GameObject;
			}
			else*/
			{
				GameObject go = Instantiate(trackerPrefab, Vector3.zero,Quaternion.identity) as GameObject;
				return go;
				
			}
		}

		public string trackerName="";

		abstract protected ITracker adaptController(GameObject go);

		void createController()
		{
			//create the keyboard controller
			GameObject go;
			{
				//Create GameObject
				go = init();
				//Add specific components
				//Register Tracker

				ITracker newTracker = null;

				try {
					newTracker = adaptController(go);
				}
				catch
				{
					newTracker = null;
				}
				if(newTracker != null)
				{
					ControllerHandler.AddTracker(newTracker);
				}
				else
				{
                    errorMsg.Add(trackerName + " not initialized");

					Object.Destroy(go);
				}
				status = false;
			}
		}

	}

}