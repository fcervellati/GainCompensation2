/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * MonoBehaviour that outputs position through a ZmqOutNode class to address:port
 * 
***********************************************************************************************************/

using UnityEngine;

public class ZmqOutputer : MonoBehaviour {

    public ITracker target;

    public string address = "*";
    public int port = 5560;
	
	void Start () {

        //Create and initialize ZmqOutnode
        zmq = new ZmqOutnode<miVRlink.Position6DofMessage>();
        zmq.init(address, port);

        target.SubscribeToPositionUpdate(positionUpdate);
        
	}
	void OnDisable()
    {
        target.UnsubscribeToPositionUpdate(positionUpdate);
    }

    public ZmqOutnode<miVRlink.Position6DofMessage> zmq;

    public void positionUpdate(Vector3 pos, Quaternion ori)
    {
        miVRlink.Position6DofMessage msg = new miVRlink.Position6DofMessage();

        pos = CoordinateTransformations.GameEngineToVector(pos);
        ori = CoordinateTransformations.GameEngineToQuaternion(ori);

        msg.timestamp_system = Time.time;
        msg.timestamp_device = Time.time;

        msg.px = pos.x;
        msg.py = pos.y;
        msg.pz = pos.z;

        msg.qw = ori.w;
        msg.qx = ori.x;
        msg.qy = ori.y;
        msg.qz = ori.z;

        zmq.sendData(msg);
    }

}
