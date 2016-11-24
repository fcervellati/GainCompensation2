/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Tracker that uses Unity.VR Tracking input and keyboard
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

public class VrController : ITracker {
		
	public float maximumSpeed = 2f;
	public UnityEngine.VR.VRNode node = UnityEngine.VR.VRNode.Head;
    
//    void Awake()
//    {
//        ControllerHandler.AddTracker(this);
//    }
//    void OnDestroy()
//    {
//        ControllerHandler.RemoveTracker(this);
//    }

    void OnEnable()
	{
		SetInitialPosition ();

	}
	Vector3 movement;
	Vector3 rootPosition;
	Quaternion rootRotation;
	override public void SubscribeToPositionUpdate(PositionUpdate positionUpdateFunction)
	{
		// Get the initial transformation that is used to neutralize the Oculus controller effect
		rootRotation = UnityEngine.VR.InputTracking.GetLocalRotation(node);
		base.SubscribeToPositionUpdate (positionUpdateFunction);
	}
	void Update () 
	{
		//get orientation from sensor
        Vector3 localPos = UnityEngine.VR.InputTracking.GetLocalPosition(node);
        Quaternion localOri = UnityEngine.VR.InputTracking.GetLocalRotation(node);
	
		movement = new Vector3(0,0,0);



		// Calculate rotation with respect to Virtual World coordinate system
		m_rotation = Quaternion.Inverse(rootRotation)*localOri;

		//if walkOnGround is disabled, set height to 1.7
		if(localPos.y != 1.7f && !WalkOnGround)
			movement.y = 1.7f+(Quaternion.Inverse(rootRotation)*localPos).y; // To be checked again

		//get movement from keyboard
		if(Input.GetKey(KeyCode.W))
		{
			movement.z += maximumSpeed*Time.deltaTime*Mathf.Cos(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
			movement.x += maximumSpeed*Time.deltaTime*Mathf.Sin(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			movement.z += -maximumSpeed*Time.deltaTime*Mathf.Cos(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
			movement.x += -maximumSpeed*Time.deltaTime*Mathf.Sin(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			movement.z += maximumSpeed*Time.deltaTime*Mathf.Sin(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
			movement.x += -maximumSpeed*Time.deltaTime*Mathf.Cos(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			movement.z += -maximumSpeed*Time.deltaTime*Mathf.Sin(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
			movement.x += maximumSpeed*Time.deltaTime*Mathf.Cos(m_rotation.eulerAngles.y*Mathf.Deg2Rad);
		}
		// Get old tracker position
		m_position = transform.position;

		m_position.y = movement.y;
		m_position.x = m_position.x + movement.x ;
		m_position.z = m_position.z + movement.z ;

		// Set new tracker position
		transform.position = m_position;
		transform.rotation = m_rotation;

		//trigger position update
		TriggerPositionUpdate(m_position,m_rotation);
		
	}
    
//	override public bool GetPosition(out Vector3 pos)
//	{
//		pos = m_position;
//		return true;
//	}
//
//	override public bool GetRotation(out Quaternion rot)
//	{
//		rot = m_rotation;
//		return true;
//	}
//
//	override public bool GetTransform(out Vector3 pos, out Quaternion rot)
//	{
//		pos = m_position;
//		rot = m_rotation;
//        return true;
//
//	}
}
