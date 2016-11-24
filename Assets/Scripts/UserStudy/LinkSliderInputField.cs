using UnityEngine;
using System.Collections;
using System;

public class LinkSliderInputField : MonoBehaviour {

	public UnityEngine.UI.Slider slider;
	public UnityEngine.UI.InputField inputField;

	void OnEnable()
	{
		slider.onValueChanged.AddListener(delegate {OnSliderChange ();});
		inputField.onEndEdit.AddListener (delegate {OnInputFieldChange ();});
	}

	void OnSliderChange()
	{
		inputField.text = slider.value.ToString ();
	}
	void OnInputFieldChange()
	{
		float temp;
		if (slider.wholeNumbers) {			
			float.TryParse(inputField.text, out temp);
			if (temp > slider.maxValue)
				temp = (int)slider.maxValue;
			else if (temp < slider.minValue)
				temp = (int)slider.minValue;	
			slider.value = (int)temp;
			inputField.text = ((int)temp).ToString();
			Debug.Log (((int)temp).ToString ());
		} else {						
			float.TryParse(inputField.text, out temp);
			if (temp > slider.maxValue)
				temp = slider.maxValue;
			else if (temp < slider.minValue)
				temp = slider.minValue;
			
			slider.value = temp;
			inputField.text = temp.ToString();
			Debug.Log (temp.ToString ());
		}

	}
}
