/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Shows a series of messages in a UI text field
 *  
***********************************************************************************************************/
using UnityEngine;
using System.Collections.Generic;

namespace MessageSystem
{
    public class Line : MonoBehaviour
    {

        public static Line Main;

        List<Message> Messages;
        
        //reference to the UI element
        public UnityEngine.UI.Text TextField;

        //is a message currently showing
        bool IsShowingMessage;

        //mode of current message
        Message.HoldMode CurrentMode;

        void OnEnable()
        {
            if (Main == null)
            {
                Main = this;
            }

            if (Messages == null)
            {
                Messages = new List<Message>();
            }
        }

        public void AddMessage(Message message)
        {

            Messages.Add(message);

            if (!IsShowingMessage || CurrentMode == Message.HoldMode.nohold)
            {
                ShowMessage();
            }
        }

        void ShowMessage()
        {
            if (TextField != null && Messages.Count > 0)
            {
                //show the text
                TextField.text = Messages[0].getMessage();

                //cancle current invoke
                CancelInvoke();
                //call new disable
                Invoke("DisableMessage", Messages[0].getDuration()); //do the invoke on the first object

                //set current mode
                CurrentMode = Messages[0].getMode();
                //remove message from list
                Messages.RemoveAt(0);
                //set IsShowingMessage flag
                IsShowingMessage = true;
            }
        }

        void DisableMessage()
        {
            if (Messages.Count > 0)
            {
                ShowMessage();
            }
            else
            {
                TextField.text = "";
                
                IsShowingMessage = false;
            }
        }

    }
}