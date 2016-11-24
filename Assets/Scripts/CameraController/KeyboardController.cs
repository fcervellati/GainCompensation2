/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 16.02.16
 * Tracker that uses keyboard and mouse
 * 
***********************************************************************************************************/


using UnityEngine;
using System.Collections;

public class KeyboardController : ITracker {

	//current walking speed
	public float maxWalkingSpeed = 2f;
	public float maxStrafeSpeed = 1f;
	public float X;
	public float Z;

	float xSensitivity = 10f;
	float ySensitivity = 5f;

	public float HeightAboveGround = 1.7f;
//
//	void Awake()
//	{
//		ControllerHandler.AddTracker(this);
//	}
//	void OnDestroy()
//	{
//		ControllerHandler.RemoveTracker(this);
//	}

	void OnEnable()
	{
		SetInitialPosition ();
	}

	void Update ()
	{
		//calculate rotations
		//get the current orientation
		// Get new position
		Vector3 rotationVec = transform.rotation.eulerAngles;

		//change rotation based on mouse input
		rotationVec.y+=Input.GetAxis("Mouse X")*xSensitivity;
		rotationVec.x-=Input.GetAxis("Mouse Y")*ySensitivity;
		rotationVec.z = 0;

		//translate to quaternion
		m_rotation = Quaternion.Euler(rotationVec);

		//calculate movement
		Vector3 movement = new Vector3(0,0,0);
		//if walkOnGround is disabled, set height to 1.7
		if(transform.position.y != HeightAboveGround && !WalkOnGround)
			movement.y = HeightAboveGround - transform.position.y;

		//get key input for walking
		float walkingSpeed = Input.GetAxis("Vertical") * maxWalkingSpeed;
		float strafeSpeed = Input.GetAxis("Horizontal") * maxStrafeSpeed;

		if(walkingSpeed!=0f)
		{
			movement.z += walkingSpeed*Time.deltaTime*Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			movement.x += walkingSpeed*Time.deltaTime*Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
		}

		if(strafeSpeed != 0f)
		{
			movement.z += -strafeSpeed*Time.deltaTime*Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
			movement.x += strafeSpeed*Time.deltaTime*Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
		}

		//update position
		m_position = transform.position + movement;

		//if walkOnGround is enabled, check height
		if(WalkOnGround)
		{
			m_position = FallToGround(m_position, HeightAboveGround);
		}

		transform.position = m_position;
		transform.rotation = m_rotation;

		//trigger position update
		TriggerPositionUpdate(m_position, m_rotation);
		X = transform.position.x;
		Z = transform.position.z;
	}
}


