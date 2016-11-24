/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Allows to add events and trigger strings
 * DEPRECATED
***********************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;


/*==========================================================================================
 * commandList classes:
 * commandList<T> handles commands with parameter
 * commandList handles commands without parameter
 * 
 * addCommand: adds a command to the list and creates new list if necessary
 * 
 * removeCommand: removes the command
 * 
 * trigger: triggers all matching commands with param as parameters
 * 
 */

[System.Obsolete("Deprecated. Use KeyboardInput instead")]
static public class CommandManager<T> {

	//Method signature
	//public delegate void KeyActionMulti(params T[] s);
	public delegate void KeyAction(T s);
	//Action dictionary
	//static Dictionary<string,KeyActionMulti> actionListMulti;
	static Dictionary<string,KeyAction> actionList;

	static public void addCommand(string name, KeyAction action)
	{
		
		if(actionList==null) //check if dictionary exists
		{
			//if not, create one
			actionList = new Dictionary<string, KeyAction>();
		}
		KeyAction value;
		if(actionList.TryGetValue(name, out value)) //try to find the value
		{
			//if found, add action to it
			actionList[name]+=action;
		}
		else
		{
			//if not add new entry
			actionList.Add(name,action);
		}

	}
	
	static public void removeCommand(string name, KeyAction action)
	{
		if(actionList!=null) //check if dictionary exists
		{
			KeyAction value;
			if(actionList.TryGetValue(name, out value)) //try to find the value
			{
				//if found, remove the action
				actionList[name]-=action; 

				if(actionList[name]==null) //check if there is an action left
				{
					//if not remove the entry
					actionList.Remove(name);
				}
			}
		}
	}
	
	static public void trigger(string name, T param)
	{
		if(actionList!=null) //check if dictionary exists
		{
			KeyAction value;
			if(actionList.TryGetValue(name, out value))
			{
				value(param);
			}
			else
			{
				Debug.LogError("Unknown command: " + name);
			}
		}
	}

	/* with using T = object[], this is not really necessary
	 * 
	//MULTI PARAMETER
	static public void addCommand(string name, KeyActionMulti<T> action)
	{
		
		if(actionListMulti==null) //check if dictionary exists
		{
			//if not, create one
			actionListMulti = new Dictionary<string, KeyActionMulti<T>>();
		}
		KeyActionMulti<T> value;
		if(actionListMulti.TryGetValue(name, out value)) //try to find the value
		{
			//if found, add action to it
			actionListMulti[name]+=action;
		}
		else
		{
			//if not add new entry
			actionListMulti.Add(name,action);
		}
		
	}
	
	static public void removeCommand(string name, KeyActionMulti<T> action)
	{
		if(actionListMulti!=null) //check if dictionary exists
		{
			KeyActionMulti<T> value;
			if(actionListMulti.TryGetValue(name, out value)) //try to find the value
			{
				//if found, remove the action
				actionListMulti[name]-=action; 
				
				if(actionListMulti[name]==null) //check if there is an action left
				{
					//if not remove the entry
					actionListMulti.Remove(name);
				}
			}
		}
	}
	
	static public void trigger(string name, params T[] param)
	{
		if(actionListMulti!=null) //check if dictionary exists
		{
			KeyActionMulti<T> value;
			if(actionListMulti.TryGetValue(name, out value))
			{
				value(param);
			}
			else
			{
				Debug.LogError("Unknown command: " + name);
			}
		}
	}*/
}

static public class CommandManager {

	//method signature
	public delegate void KeyAction();
	//action dictionary
	static Dictionary<string,KeyAction> actionListMulti;


	static public void addCommand(string name, KeyAction action)
	{
		
		if(actionListMulti==null) //check if dictionary exists
		{
			//if not, create one
			actionListMulti = new Dictionary<string, KeyAction>();
		}
		KeyAction value;
		if(actionListMulti.TryGetValue(name, out value)) //try to find the value
		{
			//if found, add action to it
			actionListMulti[name]+=action;
			
		}
		else
		{
			//if not add new entry
			actionListMulti.Add(name,action);
		}
	}
	
	static public void removeCommand(string name, KeyAction action)
	{
		if(actionListMulti!=null) //check if dictionary exists
		{
			KeyAction value;
			if(actionListMulti.TryGetValue(name, out value)) //try to find the value
			{
				//if found, remove the action
				actionListMulti[name]-=action;
				
				if(actionListMulti[name]==null) //check if there is an action left
				{
					//if not remove the entry
					actionListMulti.Remove(name);
				}
			}
		}
	}
	
	static public void trigger(string name)
	{
		if(actionListMulti!=null) //check if dictionary exists
		{
			KeyAction value;
			if(actionListMulti.TryGetValue(name, out value))
			{
				value();
			}
			else
			{
				Debug.LogWarning("Unknown command: " + name);
			}

		}
	}
}
	