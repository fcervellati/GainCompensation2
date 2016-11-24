using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StartUp
{
	public class StartUpRoutine : MonoBehaviour {

		//static bool initialized = false;

		public StartUpScript[] startupscripts;

		public string status;

		// Use this for initialization
		void Start () {
		
			StartCoroutine("startUp");

		}


		IEnumerator startUp()
		{
		

			foreach(StartUpScript s in startupscripts)
			{
				if(s!=null && s.gameObject.activeSelf)
				{

					s.StartUp();

                    float startTime = Time.time;
					while(s.IsRunning())
					{
						yield return new WaitForEndOfFrame();

                        if(Time.time-startTime>1800f)
                        {
                            s.abort();
                            break;
                        }
					}

					if(s.GetErrorMsg().Count>0)
					{
						foreach(string str in s.GetErrorMsg())
                        {   
                            Debug.Log("Error \"" + str + "\" in startup script " + s.GetName());
                            MessageSystem.Line.Main.AddMessage(new MessageSystem.Message("Error \"" + str + "\" in startup script " + s.GetName()));
                        }
					}
					else
					{
                        Logger.Print(s.GetName() + " started successfully", Logger.Type.sensors);
					    //MessageSystem.Line.main.addMessage(new MessageSystem.Message(s.GetName() + " started successfully"));
					}

                    yield return new WaitForEndOfFrame();
				}
			}
		}
	}
}
