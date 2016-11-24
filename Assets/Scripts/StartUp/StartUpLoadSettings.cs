using UnityEngine;
using System.Collections;

namespace StartUp
{
	public class StartUpLoadSettings : StartUpScript {

		static bool initialized = false;

		override public void StartUp()
		{
			if(!initialized)
			{
                SettingsHandler.Load();
				initialized = true;
			}
		}
	}
}