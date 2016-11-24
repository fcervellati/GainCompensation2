/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Base class for resets. Can subscribe to a IResetService
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

namespace Reset
{
    public abstract class ResetTechnique : MonoBehaviour
    {

        void Start()
        {
            Initialize();
            ResetServiceHandler.AddOnNewResetService(NewServiceAvailable);
        }
        void OnDestroy()
        {
            ResetServiceHandler.RemoveOnNewResetService(NewServiceAvailable);
        }

        //cycle through reset services. This should not be in this class.
        public IResetService CurrentResetService;
        public void SwitchThroughResetServices()
        {
            System.Collections.Generic.List<IResetService> providers = ResetServiceHandler.GetProviders();
            if (providers != null)
            {
                IResetService newResetService = null;

                int current = -1;
                for (int i = 0; i < providers.Count; i++)
                {
                    if (CurrentResetService == providers[i])
                    {
                        current = i;
                    }
                }
                newResetService = providers[(current + 1) % providers.Count];

                if (newResetService != null)
                {
                    MessageSystem.Line.Main.AddMessage(new MessageSystem.Message("Reset service set", 0.5f));

                    if (CurrentResetService != null)
                    {
                        CurrentResetService.RemoveOnResetUpdate(SetSignal);
                    }
                    CurrentResetService = newResetService;
                    CurrentResetService.AddOnResetUpdate(SetSignal);
                }
            }
            else
            {
                Debug.LogWarning("no providers");
            }
        }

        void NewServiceAvailable()
        {
            if (CurrentResetService == null)
                SwitchThroughResetServices();
        }

        abstract protected void Initialize();
        abstract protected void SetSignal(ResetState newState);

    }
}