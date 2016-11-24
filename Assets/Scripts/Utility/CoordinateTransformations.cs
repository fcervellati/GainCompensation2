/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Provides transformations from the standard right-handed Z up system used to the left-handed Unity system and back.
 * 
***********************************************************************************************************/

using UnityEngine;

public class CoordinateTransformations {

    //===========================================================================================================================================
    //Transformation from game engine to Unity (right hand to left hand and z up to y up)
    static Quaternion t_ge_vw = (new Quaternion(0f, -Mathf.Sqrt(0.5f), 0f, Mathf.Sqrt(0.5f))) * (new Quaternion(-Mathf.Sqrt(0.5f), 0f, 0f, Mathf.Sqrt(0.5f)));

    //===========================================================================================================================================
    //use these to change the coordinate system from the standard system to the left handed Unity system
    static public Vector3 VectorToGameEngine(Vector3 input)
    {
        return Vector3.Reflect(t_ge_vw * input, Vector3.right);
    }
    static public Quaternion QuaternionToGameEngine(Quaternion input)
    {
        Quaternion q = (t_ge_vw * input * Quaternion.Inverse(t_ge_vw));

        return new Quaternion(-q.x, q.y, q.z, -q.w);
    }

    //===========================================================================================================================================
    //use these to change the coordinate system from the left handed Unity system to the standard system
    static public Vector3 GameEngineToVector(Vector3 input)
    {
        return Quaternion.Inverse(t_ge_vw) * Vector3.Reflect(input, Vector3.right);
    }
    static public Quaternion GameEngineToQuaternion(Quaternion input)
    {
        Quaternion q = new Quaternion(-input.x, input.y, input.z, -input.w);

        return (Quaternion.Inverse(t_ge_vw) * q * t_ge_vw);
    }	
	
}
