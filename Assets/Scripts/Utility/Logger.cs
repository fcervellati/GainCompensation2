/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Offers improved debug output functionality
 * 
***********************************************************************************************************/

using UnityEngine;

public class Logger
{
    public enum Type
    {

        normal,
        network,
        settings,
        sensors
    }
    public enum Level
    {
        log,
        warning,
        error
    }

    public static void Print(string text, Type type = Type.normal, Level level = Level.log)
    {
        if (type != Type.normal)
        {
            string color = "";
                
            if(type == Type.network)
                color = "green";
            else if (type == Type.sensors)
                color = "aqua";
            else if (type == Type.settings)
                color = "orange";
                
                
            text = "<color="+color+">" + text + "</color>";
        }

        if (level == Level.error)
            Debug.LogError(text);
        else if (level == Level.warning)
            Debug.LogWarning(text);
        else
            Debug.Log(text);
    }

   public static void Print(string text, string color, Level level = Level.log)
   {
      
       text = "<color=" + color + ">" + text + "</color>";
       

       if (level == Level.error)
           Debug.LogError(text);
       else if (level == Level.warning)
           Debug.LogWarning(text);
       else
           Debug.Log(text);
   }

}
