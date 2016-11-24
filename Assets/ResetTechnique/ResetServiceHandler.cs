/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Static class that handles all IResetService instances
 * 
***********************************************************************************************************/

using System.Collections.Generic;

namespace Reset
{
    public class ResetServiceHandler
    {

        //list of all IResetServices
        static List<IResetService> providers;

        //add service to list
        public static void AddService(IResetService newService)
        {
            if (providers == null)
            {
                providers = new List<IResetService>();
            }
            providers.Add(newService);

            //inform subsribers of new service
            TriggeOnNEwResetService();
        }

        //remove service
        public static void RemoveService(IResetService newService)
        {
            if (providers != null)
            {
                providers.Remove(newService);
            }
        }

        //return list of all providers
        public static List<IResetService> GetProviders()
        {
            return providers;
        }

        //define delegate and event for NewServiceAvailable message
        public delegate void NewResetService();
        public static event NewResetService OnNewResetService;

        //subscribe to new service available update
        public static void AddOnNewResetService(NewResetService action)
        {
            OnNewResetService += action;

            if (providers != null)
                action();
        }

        //unsubscribe from new service available update
        public static void RemoveOnNewResetService(NewResetService action)
        {
            OnNewResetService -= action;
        }

        //send message to subscribers
        static void TriggeOnNEwResetService()
        {
            if (OnNewResetService != null)
            {
                OnNewResetService();
            }
        }
    }
}