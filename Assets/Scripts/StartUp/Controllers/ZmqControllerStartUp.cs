using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{

	public class ZmqControllerStartUp : StartUpController {

        public string Address = "127.0.0.1";
        public int Port = 5556;
        
		override protected ITracker adaptController(GameObject go)
		{
            if (trackerName.Length==0)
			    trackerName = "ZMQ Controller";


			//if(zmqController.isRunning())
			{
                go.name = trackerName;
                ZmqController me = go.AddComponent<ZmqController>();
                if(me!=null)
                {
                    me.Init(Address, Port);
                }
				return me;
			}
		/*	else
			{
				return null;
			}-/*/

		}
	}

}