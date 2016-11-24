/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Defines the interface for ResetServices. A ResetService provides reset informantion
 * 
***********************************************************************************************************/

namespace Reset
{
    //Delegate for the ResetState
    public delegate void ResetUpdate(ResetState status);

    //Enum of the reset command on the controller side
    public enum ResetState
    {
        RIGHT = -1,
        OFF = 0,
        LEFT = 1
    }

    /*************************************************************************************************************************
     * 
     * Interface for services that provide ResetEvents
     * 
    /*************************************************************************************************************************/
    public interface IResetService
    {

        ResetState GetResetStatus();

        //position update event
        void AddOnResetUpdate(ResetUpdate add);
        void RemoveOnResetUpdate(ResetUpdate remove);
        void TriggerResetUpdate(ResetState status);


    }
}