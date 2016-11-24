/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 *  HMD.CameraSetup implementation for the Unity VR camera
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

namespace HMD
{
    public class VrCamera : HMD.CameraSetup
    {

        void Awake()
        {
            //change the VR settings to enable Oculus mode
            UnityEngine.VR.VRSettings.loadedDevice = UnityEngine.VR.VRDeviceType.Oculus;
			UnityEngine.VR.VRSettings.enabled = true;
			UnityEngine.VR.VRSettings.showDeviceView = true;
        }
    }


}