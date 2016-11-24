using UnityEngine;
using System.Collections;

public class StudySettingsManager : MonoBehaviour {
	public event System.Action<StudySettings> OnStudySettingsChanged;
	StudySettings studySettings = new StudySettings();
	public UnityEngine.UI.Button applyButton;
	[SerializeField] LinkSliderInputField startingGain;
	[SerializeField] LinkSliderInputField increaseFactor;
	[SerializeField] LinkSliderInputField decreaseFactor;
	[SerializeField] LinkSliderInputField fixedNumberOfReversalPoints;
	[SerializeField] LinkSliderInputField usedNumberOfReversalPoints;
	[SerializeField] GameObject menu;

	void OnEnable(){
		applyButton.onClick.AddListener (delegate {
			HandleButtonClicked ();
		});
	}
	void HandleButtonClicked()
	{
		studySettings.StartingGain = startingGain.slider.value;
		studySettings.IncreaseFactor = increaseFactor.slider.value;
		studySettings.DecreaseFactor = decreaseFactor.slider.value;
		studySettings.FixedNumberOfReversalPoints = (int)fixedNumberOfReversalPoints.slider.value;
		studySettings.UsedNumberOfReversalPoints = (int)usedNumberOfReversalPoints.slider.value;

		// send event that value has changed
		StudySettingsChanged();

		//TODO how about datalogger?
	}
	void StudySettingsChanged()
	{
		var handler = OnStudySettingsChanged;
		if (handler != null) {
			handler (studySettings);
		}
	}
	public void LoadSettings(){
	}
	public void SaveSettings(){
	}
	public void ToggleSettingsMenu()
	{
		menu.SetActive (!menu.activeSelf);	
	}

}
