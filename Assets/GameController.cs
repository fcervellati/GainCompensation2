using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public void TogglePauseGame()
	{
		if (Time.timeScale == 0f) {
			Time.timeScale = 1f;
		} else {
			Time.timeScale = 0f;
		}
	}
}
