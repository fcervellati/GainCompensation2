using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StartUp{
	
	public class GetUserInfo : StartUpScript {
		
		public InputField UserName;
		public InputField UserHeight;
        public Toggle IsBlindfoldToggle;
        public Toggle IsTrainingToggle;
        public Button SubmitButton;
        public Text WarningText;
        public GameObject KeyboardInput;

        bool nameSubmitted = false;

		bool isNameValid = false;
		bool isHeightValid = false;
		void Awake()
		{
			UserName.onEndEdit.AddListener (HandleOnEndEdit);
			UserHeight.onEndEdit.AddListener (HandleOnEndHeightEdit);
			SubmitButton.onClick.AddListener (HandleButtonClicked);
            
//			(delegate(string text){
//				nameSubmitted = true;
//				status = false;
//			});
		}
		void HandleButtonClicked()
		{
			if (isNameValid && isHeightValid) {
				
				//Debug.Log ("button clicked");
                PlayerPrefs.SetInt("IsTraining", IsTrainingToggle.isOn == true?1 : 0);
                PlayerPrefs.SetInt("IsBlindfolded", IsBlindfoldToggle.isOn == true ? 1 : 0);
                status = false;
				KeyboardInput.SetActive (true);			
			} else if (!isHeightValid) {
				
				WarningText.text = "Height not valid";
			}
		}
		void HandleOnEndHeightEdit(string h)
		{	try
			{	
				float temp = System.Convert.ToSingle (h);
				if (temp < 0f) {

				} else {
					PlayerPrefs.SetFloat ("Height", temp);
					isHeightValid = true;
				}
			}
			catch {
				
			}

		}
		// Use this for initialization
		override public void StartUp () {
			// Wait until name is filled
			//Debug.Log("get user info");
			status = true;
			KeyboardInput.SetActive (false);

		}
		void HandleOnEndEdit(string text)
		{
			//Debug.Log (UserName.GetComponentInChildren<Text> ().text);
			PlayerPrefs.SetString ("Username", UserName.GetComponentInChildren<Text> ().text);
			isNameValid = true;
		}


	}
}