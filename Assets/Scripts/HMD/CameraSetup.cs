/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Abstract base class for all camera configurations
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

namespace HMD
{
	abstract public class CameraSetup : MonoBehaviour {

		public Camera MainCamera;
	}
}