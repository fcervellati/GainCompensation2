/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * This compensates the target position with the inverse of t UnityEngine.VR tracking output. THis is necessary because UnityEngine.VR autmoatically rotates cameras at the moment.
 * 
***********************************************************************************************************/

using UnityEngine;

public class VrCompensator : MonoBehaviour {

    public UnityEngine.VR.VRNode node;
    Transform target;

    //find the child with a child that has a camera component on it
    void OnEnable()
    {
        for (int t = 0; t < transform.childCount; t++)
        {
            Camera cameraRig = transform.GetChild(t).GetComponentInChildren<Camera>();
            if(cameraRig != null)
            {
                target = transform.GetChild(t);
            }
        }
    }
    public void LateUpdate()
    {
        if (target != null)
        {
            //get vr tracking position
            Vector3 localPos = UnityEngine.VR.InputTracking.GetLocalPosition(node);
            Quaternion localOri = UnityEngine.VR.InputTracking.GetLocalRotation(node);

            //set the targets position to the inverse
            target.localPosition = -(Quaternion.Inverse(localOri) * localPos);
            target.localRotation = Quaternion.Inverse(localOri);
        }
    }
}
