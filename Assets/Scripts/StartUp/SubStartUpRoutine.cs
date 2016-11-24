using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{
    public class SubStartUpRoutine : StartUpScript
    {

        //static bool initialized = false;

        public StartUpScript[] startupscripts;

        override public void StartUp()
        {
            foreach (StartUpScript s in startupscripts)
            {
                if (s != null && s.gameObject.activeSelf)
                {

                    s.StartUp();

                    if (s.GetErrorMsg().Count > 0)
                    {
                        foreach (string str in s.GetErrorMsg())
                            Debug.Log("Error \"" + str + "\" in startup script " + s.GetName());
                    }
                    else
                    {
                        Logger.Print(s.GetName() + " started successfully", Logger.Type.sensors);
                        //MessageLine.main.addMessage(new LineMessage(s.GetName() + " started successfully"));
                    }
                }
            }
        }
    }
}
