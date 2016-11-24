/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Custom editor for the CameraHandler class
 * 
***********************************************************************************************************/

using UnityEngine;
using UnityEditor;

namespace HMD
{
    [CustomEditor(typeof(CameraHandler))]
    public class CameraHandlerEditor : Editor 
    { 
        private SerializedObject serObj;

	    public void OnEnable () 
	    {
		    serObj = new SerializedObject (target);
	    }

        public override void OnInspectorGUI()
        {
            //Update target
            serObj.Update();
            //Cast to CameraHandler
            CameraHandler controller = ((CameraHandler)serObj.targetObject);

            //Get all available cameras
            EditorGUILayout.LabelField("Camera Setups");
            for (int i = 0; i < controller.GetNumberOfCameras(); i++)
            {
                CameraSetup cam = EditorGUILayout.ObjectField("\t"+i, controller.GetCamera(i), typeof(CameraSetup), true) as CameraSetup;
                if (cam != null)
                {
                    controller.SetCamera(cam, i);
                    EditorUtility.SetDirty(controller);
                }
                else
                {

                    controller.SetCamera(cam, i);
                    EditorUtility.SetDirty(controller);
                    //controller.removeCamera(i);
                }
            }
            {
                //Create empty field for new CameraSetup
                CameraSetup cam = EditorGUILayout.ObjectField("Additional Setup", null, typeof(CameraSetup), true) as CameraSetup;
                if (cam != null)
                {
                    //if something was assigned, add it to the list
                    controller.SetCamera(cam, controller.GetNumberOfCameras());
                }
            }

            //Add a button to remove empty slots in the list. THIS CHANGES INDICES IN THE LIST!
             if(GUILayout.Button("Remove empty slots"))
             {
                 for (int i = 0; i < controller.GetNumberOfCameras(); i++)
                 {
                     if(controller.GetCamera(i)==null)
                     {
                         controller.RemoveCamera(i);
                     }


                 }
             }
        }
    }
}