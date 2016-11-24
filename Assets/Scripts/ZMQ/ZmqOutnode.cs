/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Templated definition of the ZMQ output node
 * 
***********************************************************************************************************/

using NetMQ;

public class ZmqOutnode<T> {

	string address;
	int port;
	
	//ZMQ variables
	NetMQContext context;
	NetMQ.Sockets.PublisherSocket publisher;

	public void init(string address, int port)
	{
		this.address = address;
		this.port = port;

		//create context
		context = NetMQContext.Create();

		//create client
		publisher = context.CreatePublisherSocket();

		//connect publisher
		publisher.Bind("tcp://" + address + ":" + port);
		
	}


	~ZmqOutnode()
	{
		close();
	}

	public void close()
	{
		if(publisher!=null)
		{
			publisher.Disconnect("tcp://" + address + ":" + port);
		}
	}

	T lastData;

	/*float timeOut = 1; //[ms], time allowed for polling in one Unity frame (actually in one call of getNewestData)
	public void setTimeOut(float timeout)
	{
		timeOut = timeout;
	}*/

	NetMQMessage msg;
	//called for each data set
	
    public void sendData(T data)
    {

        msg = new NetMQMessage();
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        ProtoBuf.Serializer.Serialize<T>(stream,data);

        NetMQFrame frame = new NetMQFrame(stream.ToArray());

        //lastData = ProtoBuf.Serializer.Deserialize<T>(new System.IO.MemoryStream(frame.ToByteArray()));
        msg.Push(frame);
         
        publisher.SendMessage(msg);
    }
}
