using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WallWarningController))]
public class WallWarningControlEditor : Editor
{

	private SerializedObject serObj;

	public void OnEnable () 
	{
		serObj = new SerializedObject (target);
	}

    public override void OnInspectorGUI()
    {
        WallWarningController controller = ((WallWarningController)serObj.targetObject);

        ITracker realTracker = EditorGUILayout.ObjectField("Real Tracker", controller.GetRealPositionTracker(), typeof(ITracker), true) as ITracker;

        if (realTracker != null && realTracker != controller.GetRealPositionTracker())
        {
            controller.SetRealPositionTracker(realTracker);
        }

        ITracker virtualTracker = EditorGUILayout.ObjectField("Virtual Tracker", controller.GetVirtualPositionTracker(), typeof(ITracker), true) as ITracker;

        if (virtualTracker != null && virtualTracker != controller.GetVirtualPositionTracker())
        {
            controller.SetVirtualPositionTracker(virtualTracker);
        }
    }


}
