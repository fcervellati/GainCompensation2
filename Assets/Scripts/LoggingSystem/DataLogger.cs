using UnityEngine;
using System.Collections;
//using VRStandardAssets.Utils;
using System.IO;
public class DataLogger : MonoBehaviour {
	
	string currentDirectory;
	string userName;
	float currentGain;

	int runNumber=1;

	char[] ignoreChar = {'(', ')'};

	StreamWriter ETStream;
	StreamWriter[] GOStream;
	StreamWriter answerStream;
	GameObject[] GOs;

	public System.Collections.Generic.List<UserStudyData> StudyData;
	public int FlushIntervalInFrameCount = 3600;
    public string FolderName;
	System.Collections.Generic.List<StreamWriter> streamWriterList = new System.Collections.Generic.List<StreamWriter>() ;
	int[] frameCount ;
	void Start()
	{
		// create data directory for each user
		userName = PlayerPrefs.GetString ("Username");
		var rootDirectory = System.IO.Directory.GetCurrentDirectory(); 
		var dataDirectory = System.IO.Path.Combine (System.IO.Directory.GetParent (rootDirectory).ToString (), FolderName);
		currentDirectory = System.IO.Path.Combine (dataDirectory, userName);
		System.IO.Directory.CreateDirectory (currentDirectory);
		Debug.Log (currentDirectory);
		if (StudyData != null) {
			frameCount = new int[StudyData.Count];
			for (int i = 0; i < StudyData.Count; i++) {
				string str = System.IO.Path.Combine (currentDirectory, StudyData [i].FileName);
				streamWriterList.Add (new StreamWriter (str, true));
			}
		}
	}
	void OnDestroy()
	{
		for (int i = 0; i < StudyData.Count; i++) {
			if (streamWriterList [i] != null) {
				streamWriterList [i].Flush ();
				streamWriterList [i].Close ();
			}
			
		}
	}
	void Update () {
		for (int i = 0; i < StudyData.Count; i++) {
			if (StudyData [i].IsRecorded) {
				streamWriterList [i].WriteLine (StudyData [i].ToString ());
				frameCount [i] += 1;
				if (frameCount [i] == FlushIntervalInFrameCount) {
					streamWriterList [i].Flush ();
					frameCount [i] = 0;
				}
				if (StudyData [i].IsContinuous == false) {
					StudyData [i].IsRecorded = false;
				
				}

			} else {
				if (frameCount[i]>0) {
					streamWriterList [i].Flush ();
					frameCount [i] = 0;
//					streamWriterList [i].Close ();
				}
			}
		}

	}
}
