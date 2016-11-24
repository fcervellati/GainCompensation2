/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * WallWarningController
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

public class WallWarningController : IFollower
{

    [SerializeField]
    ITracker RealPosition;
    [SerializeField]
    ITracker VirtualPosition;

    //enables and disables the wallwarner
    public void ToggleWallwarner()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
            MessageSystem.Line.Main.AddMessage(new MessageSystem.Message("Wallwarners enabled!", 0.5f));
        else
            MessageSystem.Line.Main.AddMessage(new MessageSystem.Message("Wallwarners disabled!", 1f));
    }

    //set the real position tracker
    override public void SetTracker(ITracker t)
    {
        SetRealPositionTracker(t);
    }
    
    public void SetRealPositionTracker(ITracker tracker)
    {
        RealPosition = tracker;
    }

    //set the virtual position
    public void SetVirtualPositionTracker(ITracker tracker)
    {
        VirtualPosition = tracker;
    }
    public ITracker GetRealPositionTracker()
    {
        return RealPosition;
    }
    public ITracker GetVirtualPositionTracker()
    {
        return VirtualPosition;
    }

    void Update()
    {
        //if the camera is tracker or remote controlled, move so it matches the real room
        //if(HMDcontroller.m_cameraControl == cameraControl.tracker ||HMDcontroller.m_cameraControl == cameraControl.remote)
        if (RealPosition != null && VirtualPosition != null)
        {
            //Get position in the virtual environment
            Vector3 posV;
            Quaternion oriV;
            //Rewave.getPosition(out posV,out oriV);
            VirtualPosition.GetTransform(out posV, out oriV);

            //Get position in the real room
            Vector3 posR;
            Quaternion oriR;
            //Rewave.getRealPosition(out posR,out oriR);
            RealPosition.GetTransform(out posR, out oriR);

            //Calculate position and orientation of the real room in the virtual environment
            Quaternion trafo = oriV * Quaternion.Inverse(oriR);

            //Vector3 campos;
            //HMD.Controller.current.getPosition(out campos);
            transform.position = posV - trafo * posR;//+Vector3.up*(campos.y-posR.y);//+mainCamera.getOffset()
            transform.rotation = trafo;//Quaternion.FromToRotation(Vector3.forward,trafo*Vector3.forward);


            transform.localPosition = -(Quaternion.Inverse(oriR) * posR);
            transform.localRotation = Quaternion.Inverse(oriR);
        }
    }


    void CreateFromPolygon()
    {
    }
}
