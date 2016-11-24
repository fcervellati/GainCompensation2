/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * Base class for the follower. This subscribes to a tracker and gets position updates
 * 
***********************************************************************************************************/

using UnityEngine;

public abstract class IFollower : MonoBehaviour {

    public abstract void SetTracker(ITracker t);
}
