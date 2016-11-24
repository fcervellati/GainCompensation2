using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{
	public class LoadLevel : StartUpScript {

        
		override public void StartUp()
		{
            string lvlname = "Main";

                
			GlobalGameController.LoadLevel(lvlname);
			Debug.Log ("Loaded Scene");
		}
	}
}