/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 *  HMD.CameraSetup implementation for the standard Unity cameras
 * 
***********************************************************************************************************/


using UnityEngine;
using System.Collections;

namespace HMD
{

    public class DefaultCamera : HMD.CameraSetup
    {

        void Awake()
        {
            //disable Unity VR mode
            UnityEngine.VR.VRSettings.loadedDevice = UnityEngine.VR.VRDeviceType.None;
        }
    }
}