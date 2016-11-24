/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 16.02.16
 * Class that implements the (de)serialization from/to json
 * 
***********************************************************************************************************/


using UnityEngine;
using System.Collections.Generic;

public class SettingsFile {

	
	JSONObject json;


    //adds path to the json tree. Path can also be a path in which case each level is checked and added if not existing
	JSONObject DeepAdd(JSONObject obj,string path)
	{
        //find the first "/"
		int idx = path.IndexOf("/");
        
		JSONObject newObj;

		string name = path;

        //if "/" was found get the substring
        if (idx>=0)
		{
			name = path.Substring(0,idx);
		}

        //check if node already exists
		if(obj.HasField(name))
		{
            //if yes, take it
			newObj = obj.GetField(name);
		}
		else
		{
            //if no, add new node
			newObj = new JSONObject();
			obj.SetField(name,newObj);
		}

		if(idx>=0)
		{
            //recursively call DeepAdd until everything is added
			return DeepAdd(newObj,path.Substring(idx+1,path.Length-idx-1));
		}
		else
		{
            //there was no "/" in the path, this is the last level
			return newObj;
		}
	}


    //convert Settings to json tree
	public JSONObject ToJSON(Settings settings)
    {
		if(json==null)
			json = new JSONObject();


        json.SetField("display", (int)settings.displayType);

        JSONObject room = DeepAdd(json, "geometry/realroom/walls");
        Debug.LogWarning("Wall warner cannot be saved");

        List<Vector2> points = settings.GetWallWarnerPoints();

        for (int i = 0;i<points.Count;i++)
        {
            JSONObject point = new JSONObject();
            
            JSONObject x = new JSONObject(points[i].x);
            point.list.Add(x);

            JSONObject y = new JSONObject(points[i].y);
            point.list.Add(y);

            room.list.Add(point);
        }
        
        
        DeepAdd(json, "screen").SetField("width", settings.GetWidth());
        DeepAdd(json, "screen").SetField("height", settings.GetHeight());
        
		JSONObject oculus = DeepAdd(json,"oculus");

        oculus.SetField("ipd", settings.ipd);
        oculus.SetField("virtualTextureScale", settings.VrRenderScale);

		return json;
	}

    //Parse Settings from json tree
	public void ParseJSON(ref Settings settings, JSONObject input)
	{
		json = input;
		int screenint = 0;
		json.GetField(ref screenint, "display");
        settings.displayType = screenint;


        try
        { 
            JSONObject room = DeepAdd(json, "geometry/realroom/walls");

            List<Vector2> WallWarnerPoints = new List<Vector2>();
        
            for (int pts = 0; pts < room.list.Count; pts++)
            {
                float x = room.list[pts].list[0].f;

                float y = room.list[pts].list[1].f;

                //Debug.Log("" + x + ", " + y);
                WallWarnerPoints.Add(new Vector2(x, y));
            }

            settings.SetWallWarnerPoints(WallWarnerPoints.ToArray());
        }
        catch
        {
            Debug.Log("geometry/realroom/walls not found, using rect");
            JSONObject rect = DeepAdd(json, "geometry/realroom/axalignedrect");
            float roomXmin = 0.0f;
            float roomXmax = 0.0f;
            float roomZmin = 0.0f;
            float roomZmax = 0.0f;
            rect.GetField(ref roomXmin, "xmin");
            rect.GetField(ref roomXmax, "xmax");
            rect.GetField(ref roomZmin, "ymin");
            rect.GetField(ref roomZmax, "ymax");

            List<Vector2> WallWarnerPoints = new List<Vector2>();

            WallWarnerPoints.Add(new Vector2(roomXmin, roomZmin));
            WallWarnerPoints.Add(new Vector2(roomXmin, roomZmax));
            WallWarnerPoints.Add(new Vector2(roomXmax, roomZmax));
            WallWarnerPoints.Add(new Vector2(roomXmax, roomZmin));

            settings.SetWallWarnerPoints(WallWarnerPoints.ToArray());
        }
        

        int resx = 0, resy = 0;
        json.GetField("screen").GetField(ref resx, "width");
        json.GetField("screen").GetField(ref resy, "height");
        settings.SetResolution(resx, resy);

        float IPD = 0.64f;
        json.GetField("oculus").GetField(ref IPD, "ipd");
        settings.ipd = IPD;
        
        float vrRenderScale = 1.0f;
        json.GetField("oculus").GetField(ref vrRenderScale, "virtualTextureScale");
        settings.VrRenderScale = vrRenderScale;

	}
}
