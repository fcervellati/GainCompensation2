/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Provides a Monobehaviour interface to the Settings class
 * 
***********************************************************************************************************/

using UnityEngine;

public class SettingsHandler : MonoBehaviour {
    
    // load/save functions
    public static void Load()
    {
        SettingsFile file = new SettingsFile();

        //open the file and deserialize the settings
        try
        {
            string s = System.IO.File.ReadAllText("settings.json");
            file.ParseJSON(ref Settings.local, new JSONObject(s));

        }
         catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public static void Save()
    {
        SettingsFile file = new SettingsFile();
        string s = file.ToJSON(Settings.local).print();
        System.IO.File.WriteAllText("settings.json", s);
    }

    public void ToggleWalkOnGround()
    {
        Settings.local.ToggleWalkOnGround();
    }
}
