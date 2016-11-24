using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public GameObject[] gameObjects;

	public Rect viewPort = new Rect(0.8f,0.8f,0.2f,0.2f);
	public int currentCamera = 0;
	int numberOfCameras;
	void Start()
	{
		gameObjects = GameObject.FindGameObjectsWithTag ("AllCameras") ;
		numberOfCameras = gameObjects.Length;
		SetCurrentCamera (currentCamera);
	}
		
	// Update is called once per frame
	void Update () {
		
		gameObjects = GameObject.FindGameObjectsWithTag ("AllCameras") ;
		numberOfCameras = gameObjects.Length;
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			currentCamera = (currentCamera + 1) % numberOfCameras;
			SetCurrentCamera (currentCamera);
		}
	
	}
	void SetCurrentCamera(int index)
	{
		Camera camera;
		for (int i = currentCamera; i < numberOfCameras+currentCamera; i++) {
			 
			int j = i % numberOfCameras;
			camera = gameObjects [j].GetComponent<Camera> ();

			if (j == currentCamera) {
				
				camera.rect = new Rect(0.0f,0.0f,1.0f,1.0f);
				camera.enabled = false;
				camera.enabled = true;
			} 
			else {
				
				camera.rect = viewPort;
				camera.enabled = false;
				camera.enabled = true;

			}
		}
		
	}

}
