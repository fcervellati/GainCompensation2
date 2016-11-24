/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Uses the ZmqInterface for ITracker
 * 
***********************************************************************************************************/

using UnityEngine;

public class ZmqController : ITracker {

	ZmqInterface<miVRlink.Position6DofMessage> zmq;

    //consider/test if the Invoke version is the best!
	float updateTime = 0.001f; //run @ 1kHz

	bool running;
	public bool isRunning()
	{
		return running;
	}

    public string Address;
    public int Port;

	void Awake()
	{
		//if(current == null)
		{
			DontDestroyOnLoad(this);
			//current = this;
			zmq = new ZmqInterface<miVRlink.Position6DofMessage>();
			running = true;
		}

        ControllerHandler.AddTracker(this);

        if(Address!="" && Port>0)
        {
            Init(Address, Port);
        }

       doUpdate();
	}

    public void Init(string adress_, int port_)
    {
        zmq.init(adress_, port_);
    }
	void OnDestoy()
	{
        ControllerHandler.RemoveTracker(this);

        zmq.close();
		running = false;
        
    }
    

    miVRlink.Position6DofMessage output;
    Vector3 pos;
    Quaternion ori;

    void doUpdate () 
    {
        //try {
        
		
		if(zmq!=null)
		{
			if(zmq.getNewestData(out output))
			{
				//Expect righthand, X front, Z up coordinate system
                pos = CoordinateTransformations.VectorToGameEngine(new Vector3(output.px, output.py, output.pz));
                ori = CoordinateTransformations.QuaternionToGameEngine(new Quaternion(output.qx, output.qy, output.qz, output.qw));
                
				m_rotation = ori;
			
				//update height if walking on ground
				if(WalkOnGround)
				{
                    //trace from new x,z coordinates and old height, with height = new height
                    m_position = FallToGround(new Vector3(pos.x, m_position.y, pos.z),pos.y);
				}
                else
                {
                    m_position = pos;
                }
			}
			Invoke("doUpdate",updateTime);
		}
		else
		{
			Invoke("doUpdate",1f);
		}

		//trigger position update
		TriggerPositionUpdate(m_position,m_rotation);
    }
    
    override public void SetInitialPosition(Vector3 pos, Quaternion ori)
    {
    }

	//get position it rewave is running
	override public bool GetPosition(out Vector3 pos)
	{
		if(running)
		{
			pos = m_position;
			return true;
		}
		pos = Vector3.zero;
		return false;
	}
	override public bool GetRotation(out Quaternion rot)
	{
		if(running)
		{
			rot = m_rotation;
			return true;
		}
		rot = Quaternion.identity;
		return false;
	}
	override public bool GetTransform(out Vector3 pos, out Quaternion rot)
	{
		if(running)
		{
			pos = m_position;
			rot = m_rotation;
			return true;
		}
		pos = Vector3.zero;
		rot = Quaternion.identity;
		return false;
	}

	
}
