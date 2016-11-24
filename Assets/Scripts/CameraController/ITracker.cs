/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 11.02.16
 * base class of the tracker. This is a publisher for a position/orientation update and allows to walk on the ground
 * 
***********************************************************************************************************/

using UnityEngine;


abstract public class ITracker : MonoBehaviour {


	/// <summary>
	/// position to be published
	/// </summary>
	protected Vector3 m_position;
	/// <summary>
	/// rotation to be published
	/// </summary>
	protected Quaternion m_rotation;

    public GameObject marker;
    public float MaximalStepHeight = 0.5f;
	[Header("Original Transform")]
	[SerializeField] Vector3 originalPosition = new Vector3 (-3f, 1.7f, 1f);
	[SerializeField] Quaternion originalRotation = Quaternion.identity;

	void Awake()
	{
		ControllerHandler.AddTracker(this);
	}
	void OnDestroy()
	{
		ControllerHandler.RemoveTracker(this);
	}

    virtual public void SetInitialPosition(Vector3 pos, Quaternion ori)
    {
		transform.position = pos;
		transform.rotation = ori;

		m_position = transform.position;
		m_rotation = transform.rotation;
    }
	virtual public void SetInitialPosition()
	{
		transform.position = originalPosition;
		transform.rotation = originalRotation;

		m_position = transform.position;
		m_rotation = transform.rotation;
	}

	//position update event
	public delegate void PositionUpdate(Vector3 pos, Quaternion ori);
	/// <summary>
	/// Occurs when a new position is published by the tracker
	/// </summary>
	event PositionUpdate OnPositionUpdate;

    int subscribers;
    virtual public void SubscribeToPositionUpdate2(PositionUpdate positionUpdateFunction)
    {
        OnPositionUpdate += positionUpdateFunction;

    }
	virtual public void SubscribeToPositionUpdate(PositionUpdate positionUpdateFunction)
	{
        //add to counter
        subscribers++;

		//subscribe
		OnPositionUpdate += positionUpdateFunction;
		
        //trigger initial event
		positionUpdateFunction(m_position,m_rotation);
	}
    public void SubscribeToPositionUpdate(PositionUpdate fnc, Vector3 pos, Quaternion ori)
    {
        if (subscribers == 0)
            SetInitialPosition(pos, ori);

        SubscribeToPositionUpdate(fnc);
    }

	virtual public void UnsubscribeToPositionUpdate(PositionUpdate fnc)
	{
        //reduce counter
        subscribers--;

		//unsubscribe
		OnPositionUpdate -= fnc;
	}


	//remove all subscribers 
	public void ClearPositionUpdate()
	{
        subscribers = 0;
		OnPositionUpdate = null;
	}

	//is this tracker walking on the ground by raytracing
	public bool WalkOnGround {
		get {
            return Settings.local.IsWalkingOnGround();
		}
	}

	/// <summary>
	/// Raise events that the new position has been received from the tracker
	/// </summary>
	/// <param name="pos">Delta tracker position.</param>
	/// <param name="ori">Delta tracker orientation.</param>
	virtual protected void TriggerPositionUpdate(Vector3 pos, Quaternion ori)
	{
		// Make a temporary copy of the event to avoid possibility of
		// a race condition if the last subscriber unsubscribes
		// immediately after the null check and before the event is raised.
		PositionUpdate handler = OnPositionUpdate;
		if (handler != null)
		{
			handler(pos, ori);
		}
	}

	float velocity=0f;
	protected Vector3 FallToGround(Vector3 startPos, float height)
	{
        Vector3 output = startPos;
        float oldY = startPos.y;
		//find the ground right beneath the position, hover "height" above it

        Ray ray = new Ray(startPos + Vector3.down * (height - MaximalStepHeight), Vector3.down);
		RaycastHit hit;
        
		//if(Physics.Raycast(ray,out hit,3.0f))
        if(Physics.SphereCast(ray,0.4f,out hit,3.0f, Physics.DefaultRaycastLayers,QueryTriggerInteraction.Ignore))
		{
            //hit something
            if (marker==null)
            {
                marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                marker.transform.localScale = 0.1f*Vector3.one;
                Destroy(marker.GetComponent<Collider>());
            }
            marker.transform.position = hit.point;

            //update velocity
			velocity += Physics.gravity.y*Time.deltaTime;

            //calculate new position
			float newY = oldY+velocity*Time.deltaTime;

            //check if new position is below ground
			if(newY<=hit.point.y+height || startPos.y<hit.point.y)
			{
				newY=hit.point.y+height;
				velocity = 0f;
			}

            //update position
            output = new Vector3(startPos.x, newY, startPos.z);
		}
        else
        {
            //nothing hit

            //Update velocity
            velocity += Physics.gravity.y * Time.deltaTime;

            //calculate new position
            float newY = oldY + velocity * Time.deltaTime;

            //update position
            output = new Vector3(startPos.x, newY, startPos.z);
        }

        return output;

	}
    
	//get position and/or rotation methods
	virtual public bool GetPosition(out Vector3 pos)
	{
		pos = transform.position;
		return true;
	}
	virtual public bool GetRotation(out Quaternion rot)
	{
		rot = transform.rotation;
		return true;
	}
	virtual public bool GetTransform(out Vector3 pos, out Quaternion rot)
	{
		GetPosition (out pos);
		GetRotation (out rot);
		return true;
	}

    public virtual bool AllowTrackedBy(ITracker tracker)
    {
//        print("comparing " + this.name + " and " + tracker.name);
        return tracker!=this;
    }
}
