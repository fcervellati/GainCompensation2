/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Define the KeyActionPair. It contains a KeyCode and a UnityEvent. In addition it has a string description. 
 * Future extensions can include Up/Down/Continuous type.
 * 
***********************************************************************************************************/

using UnityEngine;
using System;

[Serializable]
public class KeyActionPair
{

    public KeyCode key;
    public String Description;
    public UnityEngine.Events.UnityEvent action;
}