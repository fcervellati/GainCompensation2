/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Receives miVRlink.ResetStatusMessage type messages and implements the IResetService interface
 * 
***********************************************************************************************************/

using System;
using UnityEngine;

namespace Reset
{
    public class ZmqReset : MonoBehaviour, IResetService
    {

        ZmqInterface<miVRlink.ResetStatusMessage> zmq;

        public string Address;
        public int Port;

        //on start initialize the interface and try to connect to Adress:Port
        void Start()
        {
            zmq = new ZmqInterface<miVRlink.ResetStatusMessage>();
            zmq.init(Address, Port);

            ResetServiceHandler.AddService(this);
        }

        void OnDestroy()
        {
            ResetServiceHandler.RemoveService(this);
        }

        ResetState state = 0;
        //store the last published state
        ResetState lastState;

        miVRlink.ResetStatusMessage output;

        void UpdatLate()
        {
            if (zmq != null)
            {
                if (zmq.getNewestData(out output))
                {
                    state = (ResetState)output.status;

                    if (state != lastState && OnResetUpdate != null)
                    {
                        OnResetUpdate(state);
                        lastState = state;
                    }
                }
                
            }
        }

        public ResetState GetResetStatus()
        {
            return state;
        }

        public void AddOnResetUpdate(ResetUpdate add)
        {
            OnResetUpdate += add;
        }

        public void RemoveOnResetUpdate(ResetUpdate remove)
        {
            OnResetUpdate -= remove;
        }

        public void TriggerResetUpdate(ResetState status)
        {
            if (OnResetUpdate != null)
            {
                OnResetUpdate(status);
            }
        }

        event ResetUpdate OnResetUpdate;

    }
}