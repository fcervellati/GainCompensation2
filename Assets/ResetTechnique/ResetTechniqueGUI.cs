/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change 11.02.16
 * Receives miVRlink.ResetStatusMessage type messages and implements the IResetService interface for the new Unity UI
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections;

namespace Reset
{
    public class ResetTechniqueGUI : ResetTechnique
    {
        //this might be of use for other classes, but currently it is only used here
        enum ResetGuiState
        {
            RIGHT = -1,
            OFF = 0,
            LEFT = 1,
            DONE
        }
        
        //reference to all Images used to show the reset symbol
        public UnityEngine.UI.Image[] guiField;

        //icons for left, right and done state
        public Sprite rightIcon;
        public Sprite leftIcon;
        public Sprite doneIcon;

        //time the done icon is visible
        public float FadeTimeAfterReset = 1.0f;
        float counter;

        //current gui state
        ResetGuiState GuiState = ResetGuiState.OFF;

        //initialize, disable all images
        protected override void Initialize()
        {
            for (int i = 0; i < guiField.Length; i++)
            {
                guiField[i].gameObject.SetActive(false);
            }
        }

        

        protected override void SetSignal(ResetState newState)
        {
            //translate the message to the guistate enum
            if (newState == ResetState.LEFT) //lib signals left
            {
                GuiState = ResetGuiState.LEFT;
            }
            else if (newState == ResetState.RIGHT) // lib signals right
            {
                GuiState = ResetGuiState.RIGHT;
            }
            else if ((GuiState == ResetGuiState.LEFT || GuiState == ResetGuiState.RIGHT) && newState == ResetState.OFF) //lib signaled last frame, but not now. Start counting
            {
                GuiState = ResetGuiState.DONE;

                //hide done icon later
                Invoke("HideDoneIcon",FadeTimeAfterReset);
            }

            UpdateIcon();
        }

        void HideDoneIcon()
        {
            GuiState = ResetGuiState.OFF;
            UpdateIcon();
        }

        //update the icons
        void UpdateIcon()
        {
            Sprite icon = doneIcon;
            bool active = false;

            if (GuiState == ResetGuiState.LEFT)
            {
                icon = leftIcon;
                active = true;
            
            }
            else if (GuiState == ResetGuiState.RIGHT)
            {
                icon = rightIcon;
                active = true;

            }
            else if (GuiState == ResetGuiState.DONE)
            {
                icon = doneIcon;
                active = true;

            }
            else if (GuiState == ResetGuiState.OFF)
            {
                icon = doneIcon;
                active = false;

            }

            for (int i = 0; i < guiField.Length; i++)
            {
                guiField[i].sprite = icon;
                guiField[i].gameObject.SetActive(active);
            }
        }
    }

}