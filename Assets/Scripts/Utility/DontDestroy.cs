/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Keeps GameObject after scene change
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Awake() 
	{
		DontDestroyOnLoad(this);
	}

}
