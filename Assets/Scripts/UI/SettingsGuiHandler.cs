/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Has references to GUI elements and updates them every "updatetime" seconds
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

public class SettingsGuiHandler : MonoBehaviour
{
    public float updatetime = 1.0f;
    int framecount;

    //GameObject containing all elements (used for enabling/disabling)
    public GameObject panel;

    //References to Text fields
    public UnityEngine.UI.Text resolutionText;
    public UnityEngine.UI.Text vSyncText;
    public UnityEngine.UI.Text aaText;
    public UnityEngine.UI.Text fpsText;
    public UnityEngine.UI.Text ipdText;
    public UnityEngine.UI.Text vrMode;

    //FUTURE FEATURE: have multiple sizes of display: fullscreen, small in the corner and off.
    /*enum DisplayMode
    {
        max, 
        min,
        off,
        NUMBEROFMODES
    }
    DisplayMode mode;*/
        
    // Use this for initialization
    void Awake()
    {
        
        framecount = Time.frameCount;
        Invoke("DoUpdate", updatetime);

    }

    // Update is called once per frame
    void DoUpdate()
    {
        int vSync = QualitySettings.vSyncCount;
        int aa = QualitySettings.antiAliasing;
        string af = QualitySettings.anisotropicFiltering.ToString();
        float fps = ((float)(Time.frameCount - framecount)) / updatetime;
        framecount = Time.frameCount;
        resolutionText.text = "" + Screen.width + " x " + Screen.height;
        vSyncText.text = "vSync: " + vSync;
        aaText.text = "AA: " + aa + ", AF: " + af;
        fpsText.text = "fps: " + fps;
        ipdText.text = "IPD: " + Settings.local.ipd * 1000f + " mm";
        vrMode.text = "VrMode: " + UnityEngine.VR.VRSettings.loadedDevice + ", " + (UnityEngine.VR.VRSettings.enabled ? "enabled" : "disabled");
        //update every second
        Invoke("DoUpdate", updatetime);
    }

    public void ToggleDisplay()
    {
        //mode = (DisplayMode)((((int)mode) + 1) % (int)DisplayMode.NUMBEROFMODES);


        enabled = !enabled;
        panel.SetActive(enabled);
        
    }
}
