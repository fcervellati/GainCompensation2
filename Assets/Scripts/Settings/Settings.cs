/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 16.02.16
 * Class that stores all settings information and provides access to it. It also provides subscribers to keep track of changes
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public class Settings {

    //local settings (in contrast to network synced settings
    public static Settings local = new Settings();
    
    //========================================================================================================================================================


    void SetDefaults()
    {
        displayType = 0;// DisplayType.screen;
        ScreenWidth = 1280;
        ScreenHeight = 800;

        ipd = 0.064f;
        VrRenderScale = 1.0f;

        FieldOfView = 60;

        WallWarnerPoints.Clear();
        WallWarnerPoints.Add(new Vector2(0f, 0f));
        WallWarnerPoints.Add(new Vector2(-1f, 0f));
        WallWarnerPoints.Add(new Vector2(-1f, 1f));
        WallWarnerPoints.Add(new Vector2(0f, 1f));

    }

    //======================================================================================================
    //EVENTS

    //======================================================================================================
    //Wallwarner

    List<Vector2> WallWarnerPoints;

    public void SetWallWarnerPoints(Vector2[] points)
    {
        if (WallWarnerPoints == null)
        {
            WallWarnerPoints = new List<Vector2>();
        }

        WallWarnerPoints.Clear();
        for (int i = 0; i < points.Length; i++)
        {
            WallWarnerPoints.Add(points[i]);
        }

        TriggerChangeWallWarner();
    }
    

    public delegate void ChangeWallWarner(Vector2[] ipd);
    event ChangeWallWarner OnChangeWallWarner;
    public void AddOnChangeWallWarner(ChangeWallWarner inputEvent)
    {
        OnChangeWallWarner += inputEvent;

        if (WallWarnerPoints != null)
        {
            inputEvent(WallWarnerPoints.ToArray());
        }
    }
    public void RemoveOnChangeWallWarner(ChangeWallWarner inputEvent)
    {
        OnChangeWallWarner -= inputEvent;
    }
    void TriggerChangeWallWarner()
    {
        if (OnChangeWallWarner != null)
            OnChangeWallWarner(WallWarnerPoints.ToArray());
    }
    public List<Vector2> GetWallWarnerPoints()
    {
        if (WallWarnerPoints == null)
            return null;

        return new List<Vector2>(WallWarnerPoints);
    }
   
    //======================================================================================================
    //IPD
    float Ipd;
	public float ipd
	{ 
		set
		{
			Ipd = value;
            TriggerChangeIPD();

		}
		get
		{
			return Ipd;
		}
	}
    
    public delegate void ChangeIPD(float ipd);
	event ChangeIPD OnChangeIpd;
	public void AddOnChangeIPD(ChangeIPD inputEvent)
	{
		OnChangeIpd+=inputEvent;
		inputEvent(ipd);
	}
	public void RemoveOnChangeIPD(ChangeIPD inputEvent)
	{
		OnChangeIpd-=inputEvent;
	}
    void TriggerChangeIPD()
    {
        if (OnChangeIpd != null)
            OnChangeIpd(ipd);
    }


    //======================================================================================================
    //Resolution
    public delegate void ChangeResolution(int newWidth, int newHeight);
    event ChangeResolution OnChangeResolution;
	public void AddOnChangeResolution(ChangeResolution inputEvent)
	{
		OnChangeResolution+=inputEvent;
		inputEvent(ScreenWidth, ScreenHeight);
	}
	public void RemoveOnChangeResolution(ChangeResolution inputEvent)
	{
		OnChangeResolution-=inputEvent;
	}
    void TriggerChangeResolution()
    {
        if(OnChangeResolution!=null)
            OnChangeResolution(ScreenWidth, ScreenHeight);
    }
    int ScreenWidth;
    int ScreenHeight;
    public bool SetResolution(int screenWidth, int screenHeight)
    {
        //Check whether we still need to change something		
        if (ScreenWidth != screenWidth || ScreenHeight != screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            //Trigger the update event
            TriggerChangeResolution();
        }

        return true;
    }

    public int GetWidth()
    {
        return ScreenWidth;
    }
    public int GetHeight()
    {
        return ScreenHeight;
    }


    //======================================================================================================
    //FULLSCREEN
    public delegate void ChangeFullscreen(bool fullscreen);
    event ChangeFullscreen OnChangeFullscreen;
    public void AddOnChangeFullscreen(ChangeFullscreen inputEvent)
    {
        OnChangeFullscreen += inputEvent;
        inputEvent(Fullscreen);
    }
    public void RemoveOnChangeFullscreen(ChangeFullscreen inputEvent)
    {
        OnChangeFullscreen -= inputEvent;
    }
    void TriggerChangeFullscreen()
    {
        if(OnChangeFullscreen!=null)
            OnChangeFullscreen(Fullscreen);
    }
    
    bool Fullscreen;
    public void SetFullscreen(bool fullscreen)
    {
        if (Fullscreen != fullscreen)
        {
            Fullscreen = fullscreen;
            TriggerChangeFullscreen();
        }
    }
    //======================================================================================================
    //HMD
    public delegate void ChangeDisplay(int newType);
	event ChangeDisplay OnChangeDisplay;
	public void AddOnChangeScreen(ChangeDisplay inputEvent)
	{
		OnChangeDisplay+=inputEvent;
		inputEvent(GetHMD());
	}
	public void RemoveOnChangeScreen(ChangeDisplay inputEvent)
	{
		OnChangeDisplay-=inputEvent;
	}
    void TriggerOnChangeDisplay()
    {
        if (OnChangeDisplay!=null)
            OnChangeDisplay(DisplayType);
    }


    int DisplayType = 0;
    public int displayType
    {
        set
        {
            DisplayType = value;
            TriggerOnChangeDisplay();
        }
        get
        {
            return DisplayType;
        }
    }

    //set the screen type. Default is "screen", the others are specific HMD types. For the HMDs  their native resolution is enforced
    public void SetHMD(int newType)
    {
        //store the settings variable
        displayType = newType;

        //Trigger the update event
        if (OnChangeDisplay != null)
        {
            OnChangeDisplay(newType);
        }
    }

    public int GetHMD()
    {
        return DisplayType;
    }
    
    //======================================================================================================
    //FOV
    float FieldOfView = 60.0f;
    
    
    public float GetFieldOfView()
    {
        return FieldOfView;
    }

    public void SetFieldOfView(float value)
    {
        FieldOfView = value;
        //TriggerOnCHangeFieldOfView();
    }

    
//========================================================================================================================================================
	/*//MapState was used to switch different screen layouts. This will return for the demo system
    public enum MapState {
		off,
		smallLeftBottom,
		bigLeft,
		fullMap,
		NUMBEROFSTATES
	}
	public void SetMapState(MapState newMapState)
	{
		if(newMapState>=MapState.NUMBEROFSTATES)
			newMapState = 0;

		mapState = newMapState;
		OnChangeMapState(mapState);
	}
	public MapState GetMapState()
	{
		return mapState;
	}
	
	public void CycleMapState()
	{

		SetMapState(GetMapState()+1);
	}

	public MapState mapState;

	public delegate void ChangeMapState(MapState newState);
	event ChangeMapState OnChangeMapState;
	public void AddOnChangeMapState(ChangeMapState inputEvent)
	{
		OnChangeMapState+=inputEvent;
		inputEvent(mapState);
	}
	public void RemoveOnChangeMapState(ChangeMapState inputEvent)
	{
		OnChangeMapState-=inputEvent;
	}*/
//========================================================================================================================================================
	bool WalkOnGround;
	public bool IsWalkingOnGround()
	{
		return WalkOnGround;
	}
	public void ToggleWalkOnGround()
	{
		WalkOnGround = ! WalkOnGround;
	}
	public void SetWalkOnGround(bool mode)
	{
		WalkOnGround = mode;
	}

//========================================================================================================================================================
	//VR
    public float VrRenderScale
    {
        set
        {
            UnityEngine.VR.VRSettings.renderScale = value;
        }
        get
        {
            return UnityEngine.VR.VRSettings.renderScale;
        }
    }

}
