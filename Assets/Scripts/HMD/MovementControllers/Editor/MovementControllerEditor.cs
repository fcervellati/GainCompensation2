/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Custom editor for the MovementController
 * 
***********************************************************************************************************/

using UnityEditor;

namespace HMD
{
    [CustomEditor(typeof(MovementController))]
    public class MovementControllerEditor : Editor 
    { 
        private SerializedObject serObj;

        //Get the target object
	    public void OnEnable () 
	    {
		    serObj = new SerializedObject (target);
	    }

        public override void OnInspectorGUI()
        {
            //update the target
            serObj.Update();
            //cast to MovementController
            MovementController controller = ((MovementController)serObj.targetObject);

            //get content of the editor field
            ITracker newTracker = EditorGUILayout.ObjectField("Tracker", controller.GetTracker(), typeof(ITracker), true) as ITracker;

            //if content changed ...
            if (newTracker != null && newTracker != controller.GetTracker())
            {
                //... assign new tracker
                controller.SetTracker(newTracker);
            }

        
        }
    }
}