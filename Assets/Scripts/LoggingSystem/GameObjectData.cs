using System;
using UnityEngine;
// Attach this script to a gameobject to collect data from it
public class GameObjectData:UserStudyData
{	



	void OnEnable()
	{
		fileName = gameObject.name + ".txt";
		IsContinuous = true;
	}
	public GameObjectData ()
	{
		
	}

	public override string ToString ()
	{
		// Time StaircaseName RunNumber Gain
		dataText = base.ToString();
		dataText += gameObject.transform.position.ToString (stringFormat).Trim (ignoreChar) + " " + gameObject.transform.rotation.eulerAngles.ToString (stringFormat).Trim (ignoreChar);
		return dataText;
	}

}


