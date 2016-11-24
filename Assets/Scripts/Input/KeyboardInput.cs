/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Has a list of KeyActionPairs that are checked every frame and triggers the corresponding action
 * 
***********************************************************************************************************/

//DOTO change keyActionPairDown to List<> and write custom editor

using UnityEngine;

public class KeyboardInput : MonoBehaviour {

    public KeyActionPair[] keyActionPairDown;
    
    void Update()
    {
        for(int k = 0; k < keyActionPairDown.Length;k++)
        {
            if(Input.GetKeyDown(keyActionPairDown[k].key))
            {
                keyActionPairDown[k].action.Invoke();
            }
        }

    }

    /*
    public void addKeyActionPair(KeyActionPair kap)
    {
    }

    public void removeKeyActionPair(KeyActionPair kap)
    { 
    }
    */
}
