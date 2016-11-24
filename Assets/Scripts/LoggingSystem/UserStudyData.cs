using System;
using UnityEngine;
/// <summary>
/// Base class for data that will be recorded by DataLogger class
/// </summary>
public class UserStudyData:MonoBehaviour
{
	protected string fileName; //filename to be used to save the data in
	protected string stringFormat = "0.#####";
	protected char[] ignoreChar = {'(', ')'};
	protected string dataText;
	protected float currentGain;
	protected string staircaseName;
	protected int runNumber=0;
	public int FlushIntervalInFrameCount;

	public string FileName {get { return fileName; }} 
	public UserStudyData ()
	{
	}
	public bool IsRecorded;
	public bool IsContinuous;
	public virtual void UpdateData(float gain, string name, int run)
	{
		runNumber = run;
		currentGain = gain;
		staircaseName  = name;
	}
	public override string ToString()
	{
		dataText = runNumber.ToString () + " ";
		dataText += staircaseName + " ";
		dataText += Time.realtimeSinceStartup.ToString () + " ";
		dataText += currentGain.ToString () + " ";
		return dataText;
	}
}


