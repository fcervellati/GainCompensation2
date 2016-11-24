using UnityEngine;
using System.Collections;

namespace StartUp
{
	public class LoadAssetOnStartUp : StartUpScript {

        public string AssetName;

		public override void StartUp ()
		{
            AssetBundle bundle = AssetBundle.LoadFromFile(AssetName);
            bundle.LoadAllAssets();

        }

	}
}