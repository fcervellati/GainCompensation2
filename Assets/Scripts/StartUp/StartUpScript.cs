using UnityEngine;
using System.Collections;

using System.Collections.Generic;

namespace StartUp
{

abstract public class StartUpScript : MonoBehaviour{

	abstract public void StartUp();

	public string GetName()
	{
		return name;
	}

	protected List<string> errorMsg= new List<string>();
    public List<string> GetErrorMsg()
	{
		return errorMsg;
	}

	protected string statusString="";
	public string GetStatus()
	{
		return statusString;
	}

	protected bool status = false;
	public bool IsRunning()
	{
		return status;
	}

    virtual public void abort()
    {

    }
}

}