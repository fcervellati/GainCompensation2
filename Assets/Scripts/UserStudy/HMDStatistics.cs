using UnityEngine;
using System.Collections;

public class HMDStatistics : MonoBehaviour {

	public void Toggle()
	{
		gameObject.SetActive(!gameObject.activeSelf);
	}

}
