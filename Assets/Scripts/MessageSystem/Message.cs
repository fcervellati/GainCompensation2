/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Struct to contain a line feed message
 * 
***********************************************************************************************************/
namespace MessageSystem
{
    public class Message
    {

        public Message(string message)
        {
            this.message = message;
            this.duration = 2f;
            this.mode = HoldMode.nohold;
        }

        public Message(string message, float duration)
        {
            this.message = message;
            this.duration = duration;
            this.mode = HoldMode.hold;
        }

        public Message(string message, float duration, HoldMode mode)
        {
            this.message = message;
            this.duration = duration;
            this.mode = mode;
        }

        public Message(string message, HoldMode mode)
        {
            this.message = message;
            this.duration = 2f;
            this.mode = mode;
        }
        string message;
        public string getMessage()
        {
            return message;
        }
        float duration;
        public float getDuration()
        {
            return duration;
        }
        HoldMode mode;
        public enum HoldMode
        {
            hold,
            nohold
        }
        public HoldMode getMode()
        {
            return mode;
        }
    }
}