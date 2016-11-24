using UnityEngine;
using System.Collections;

public class GlobalGameController : MonoBehaviour {

	//only this method is allowed to load levels in oder to keep networked players consistent!
public static void LoadLevel(string name)
	{

        UnityEngine.SceneManagement.SceneManager.LoadScene(name);

	}
    
	public static void LoadLevel(int i)
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
		
	}
}
