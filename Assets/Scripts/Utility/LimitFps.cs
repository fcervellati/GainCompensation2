/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 16.02.16
 * Limits the fps to Fps while enabled
 * 
***********************************************************************************************************/

using UnityEngine;

public class LimitFps : MonoBehaviour {

    public int Fps = 60;

    void OnEnable()
    {
        Application.targetFrameRate = Fps;
        Logger.Print("LIMIT FPS TO " + Application.targetFrameRate, Logger.Type.settings);

    }
    
    void OnDisable()
    {
        Application.targetFrameRate = -1;
        Logger.Print("FPS unlimited", Logger.Type.settings);
    }
	
}
