using UnityEngine;
using System.Collections;

namespace StartUp
{
	public class EnableOnStartup : StartUpScript {

		public GameObject[] startUpObject;

		public override void StartUp ()
		{
			for(int o = 0;o<startUpObject.Length;o++)
                startUpObject[o].SetActive(true);
		}

	}
}