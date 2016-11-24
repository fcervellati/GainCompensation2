/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Check if the start scene was executed
 * 
***********************************************************************************************************/

using UnityEngine;

public class InitChecker : MonoBehaviour {

	void Start () 
	{
		#if UNITY_EDITOR

            if (GameObject.FindObjectOfType<HMD.CameraSetup>() == null)
			    GlobalGameController.LoadLevel(0);

		#endif

		GameObject.Destroy(gameObject);

	}	

}
