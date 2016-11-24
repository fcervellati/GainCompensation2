/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Handles switching between camera types
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

namespace HMD
{
    //Handles camera switching
    public class CameraHandler : MonoBehaviour{



        void Start()
        {
            //subscribe to the SetHMD call
            Settings.local.AddOnChangeScreen(SetHMD);

            //subscribe to the changeIPD call, not used anymore, ipd cannot be set from Unity
            //		settings.addOnChangeIPD(changeIPD);
        }

        void OnDestroy()
        {
            //unsubscribe from the SetHMD call
            Settings.local.RemoveOnChangeScreen(SetHMD);

            //unsubscribe from the changeIPD call, not used anymore, ipd cannot be set from Unity
            //		settings.removeOnChangeIPD(changeIPD);

        }

        //===================================================================================================
        //attributes
        //list of all cameras available
        [SerializeField]
        List<CameraSetup> AvailableCameras = new List<CameraSetup>();
        //reference to the current camera
        public HMD.CameraSetup ActiveCamera;


    //===================================================================================================
    //methods to handle the list of available cameras
        //returns camera i 
        public HMD.CameraSetup GetCamera(int i)
        {
            if(i>=0 && i<AvailableCameras.Count)
            {
                return AvailableCameras[i];
            }
            return null;
        }

        //adds or replaces camera in slot i
        public void SetCamera(HMD.CameraSetup cam, int i)
        {
            if(i>=0 && i<AvailableCameras.Count)
            {
                AvailableCameras[i] = cam;
            }
            else if(i>=AvailableCameras.Count)
            {
                AvailableCameras.Add(cam);
            }
        }

        //removes camera i
        public void RemoveCamera(int i)
        {
            if (i >= 0 && i < AvailableCameras.Count)
            {
                AvailableCameras.RemoveAt(i);
            }
        }

        //return number of available cameras
        public int GetNumberOfCameras()
        {
            return AvailableCameras.Count;
        }


    //===================================================================================================

        //local method to change the dispay type. Should not be called except by OnChangeScreen
        public void SetHMD(int typeNr)
        {
            //check if typeNr is valid
            if (typeNr < 0 || typeNr > AvailableCameras.Count)
            {
                Debug.LogError("Camera index out of range, set camera = 0");
                typeNr = 0;
            }
            else
            {
                Debug.Log("Camera set to " + typeNr);
            }

            //check if cammera is not set or something changes
            if (ActiveCamera == null || AvailableCameras[typeNr]!= ActiveCamera)
            {
                //check if camera was set
                if (ActiveCamera != null)
                {
                    //if yes remove old camera
                    GameObject.Destroy(ActiveCamera.gameObject);
                }

                //create new camera
                ActiveCamera = Instantiate(AvailableCameras[typeNr]) as HMD.CameraSetup;

                //make the new camera a child of this GameObject
                ActiveCamera.transform.parent = transform;
                
                //set the local position to identity
                ActiveCamera.transform.localPosition = Vector3.zero;
                ActiveCamera.transform.localRotation = Quaternion.identity;
            }

            //if the camera was set successfully
            if (ActiveCamera != null)
            {
                //find all UIs and set the new camera as the worldCamera
                UnityEngine.Canvas[] canv = GameObject.FindObjectsOfType<UnityEngine.Canvas>();
                for (int i = 0; i < canv.Length;i++)
                {
                    canv[i].worldCamera = ActiveCamera.MainCamera;
                }
            }
        }
	}

}