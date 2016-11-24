/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Templated definition of the NetMQ publisher subscriber classes
 * 
***********************************************************************************************************/

using System; //Required for TimeSpan
using NetMQ;

public class ZmqInterface<T> {

	string address;
	int port;
	
	//ZMQ variables
	NetMQContext context;
	NetMQ.Sockets.SubscriberSocket client;

	public void init(string address, int port)
	{
		
		this.address = address;
		this.port = port;
		
		//create context
		context = NetMQContext.Create();
		
		//create client
		client = context.CreateSubscriberSocket();
		//subscribe (must be before Connect!)
		client.Subscribe("");
		//connect client
		client.Connect("tcp://" + address + ":" + port);
		
		//add "getData" as callback
		client.ReceiveReady += getData;
	}


	~ZmqInterface()
	{
		close();
	}

	public void close()
	{
		if(client!=null)
		{
			client.Disconnect("tcp://" + address + ":" + port);
		}
	}

	T lastData;

	NetMQMessage msg;
	//called for each data set
	void getData(object sender, NetMQ.NetMQSocketEventArgs e)
	{
		msg = client.ReceiveMessage();

	}

	//Poll the buffer as long as there is new data. If there is new data return true, otherwise return false.
	//Only the last data sample is kept!
	public bool getNewestData(out T output)
	{
        if (client != null)
        {
            bool newDataAvailable = false;

            //note: if there is a situation where a huge backlog exists, add timeOut/System.Diagnostics.Stopwatch and do polling over multiple Unity frames
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            //not sure what happens inside Poll, but if data is available getData is called immediately and the code continues in the while loop body
            while (client.Poll(TimeSpan.Zero))//try to poll data, but don't wait if there is none
            {
                newDataAvailable = true; //we have found new data in this frame

                /*if(sw.ElapsedMilliseconds>timeOut)//check time
                {
                    //if time is up, stop the clock and break
                    sw.Stop ();
                    break;
                }*/

            }

            //if new data was received, deserialize it and return
            if (newDataAvailable)
            {
                NetMQFrame frame = msg.Pop();
                
                lastData = ProtoBuf.Serializer.Deserialize<T>(new System.IO.MemoryStream(frame.ToByteArray()));
            }

            output = lastData;


            return newDataAvailable;
        }
        output = lastData;
        return false;
	}




}
