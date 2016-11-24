using System;
using UnityEngine;
public class TransformationMatrix
{
	Vector3 m_origin;
	Quaternion m_rotation;
	public Vector3 Origin{
		get{
			return m_origin;
		}
		set{
			m_origin = value;
		}
	}
	public Quaternion Rotation{
		get{
			return m_rotation;
		}
		set{
			m_rotation = value;
		}
	}
	public TransformationMatrix (Quaternion rotation, Vector3 origin)
	{
		m_origin = origin;
		m_rotation = rotation;
	}
	public TransformationMatrix()
	{
		m_origin = Vector3.zero;
		m_rotation = Quaternion.identity;
	}
	public TransformationMatrix Multiply(TransformationMatrix A)
	{
		return new TransformationMatrix(m_rotation * A.m_rotation, m_origin + m_rotation * A.Origin);
	}
	public TransformationMatrix Inverse()
	{
		return new TransformationMatrix(Quaternion.Inverse(m_rotation),Quaternion.Inverse(m_rotation)*(-m_origin));			
	}

}


