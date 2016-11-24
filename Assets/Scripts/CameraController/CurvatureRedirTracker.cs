using UnityEngine;
using System.Collections;

public class CurvatureRedirTracker: RedirTracker
{
	public CameraController camScript;
	public float Gain = 1.0f;
	float alpha;
	float cameraStepLength;
	float realStepLength;
	float anglex, angley, anglez;

	override protected void CalculateRedirPosition(Vector3 newPosition, Quaternion newRotation){
		// inherited class implements its own redirection algorithm
		// old real world position
		Vector3 oldPosition = transform.position;
		Quaternion oldRotation = transform.rotation;
		// new real world position
		transform.position = newPosition;
		transform.rotation = newRotation;

		// old virtual world position
		if (currentSubscriber != null) {
			//		currentSubscriber.transform.position = m_position;
			anglex = transform.rotation.eulerAngles.x;
			angley = currentSubscriber.transform.rotation.eulerAngles.y;
			anglez = transform.rotation.eulerAngles.z;
			currentSubscriber.transform.rotation = Quaternion.Euler (anglex, angley, anglez);

			followerPosition = currentSubscriber.transform.position;
			followerRotation = currentSubscriber.transform.rotation;
			
		}

//		Debug.Log (Time.realtimeSinceStartup.ToString() + "   Curvature gain  " + CurvatureGain.ToString ());
		// Calculate alpha - angle of rotation
/*		alpha = CurvatureGain * (newPosition-oldPosition).magnitude;
		alpha = Mathf.Rad2Deg * alpha;

		// Make rotational matrix Rq
		var Rq = Quaternion.AngleAxis(alpha, Vector3.up);
	
		// Output to send to camera
		// Compute R_vp new (rotation of camera in virtual world coordinate system)
		m_rotation = Rq * followerRotation * Quaternion.Inverse(oldRotation) * newRotation;
*/
		// Compute x_vp new (position of camera in virtual world coordinate system): numerical errors with quaternion, length is not always exactly 1
		//Gain = camScript.CurrGain;
		m_position = Gain * (newPosition-oldPosition) + followerPosition;

		if ((newPosition - oldPosition).magnitude > 0.01f) {
				
			cameraStepLength = (m_position - followerPosition).magnitude;
			realStepLength = (newPosition - oldPosition).magnitude; 
		}

		if (WalkOnGround) 
		{
			//trace from new x,z coordinates and old height, with height = new height
			m_position = FallToGround(m_position, m_position.y);
		}  
//		Debug.Log ("I was here");
		TriggerPositionUpdate (m_position, newRotation);

	}
}