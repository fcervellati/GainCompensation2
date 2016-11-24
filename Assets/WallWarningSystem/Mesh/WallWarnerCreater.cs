/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 16.02.16
 * Creates a mesh wallwarner from Vector2 array
 * 
***********************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public class WallWarnerCreater : MonoBehaviour {

    //lower offset
    public float lower = -1f;

    //upper offset
    public float higher = 3f;

    //uv scaling
    public float meterPerTexture = 1f;


    public void CreateWallWarner(Vector2[] points)
    {

        Mesh mesh = new Mesh();

        List<Vector3> vert = new List<Vector3>();
        List<int> idx = new List<int>();
        List<Vector2> uv = new List<Vector2>();

        List<float> length = new List<float>();

        for (int i = 0; i <= points.Length; i++)
        {
            vert.Add(new Vector3(points[i% points.Length].x, lower, points[i% points.Length].y));
            vert.Add(new Vector3(points[i% points.Length].x, higher, points[i% points.Length].y));

            if (i == 0)
            {
                length.Add(0.0f);
            }
            else
            {
                length.Add(length[length.Count - 1] + Vector2.Distance(points[i% points.Length], points[i - 1]));
            }
        }
        
        for (int i = 0; i <= points.Length; i++)
        {
            idx.Add(2 * i % vert.Count);
            idx.Add((2 * i + 1) % vert.Count);
            idx.Add((2 * i + 3) % vert.Count);

            idx.Add(2 * i % vert.Count);
            idx.Add((2 * i + 3) % vert.Count);
            idx.Add((2 * i + 2) % vert.Count);
        }

        for (int i = 0; i <= points.Length; i++)
        {
            uv.Add(new Vector2(length[i] / meterPerTexture, 0.0f));
            uv.Add(new Vector2(length[i] / meterPerTexture, (higher - lower) / meterPerTexture)); 
        }
        
        mesh.SetVertices(vert);
        mesh.SetIndices(idx.ToArray(), MeshTopology.Triangles, 0);

        mesh.SetUVs(0,uv);

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }

 
    void Start ()
    {
        Settings.local.AddOnChangeWallWarner(CreateWallWarner);
    }
    void OnDestroy()
    {
        Settings.local.RemoveOnChangeWallWarner(CreateWallWarner);
    }
    
    //This should go to settings editor class once this is implemented
    void OnDrawGizmos()
    {
        List<Vector2> WallWarnerPoints = Settings.local.GetWallWarnerPoints();
        if (WallWarnerPoints!=null)
        {

            for (int i = 0; i <= WallWarnerPoints.Count; i++)
            {
                Vector3 vec1 = transform.localToWorldMatrix*(new Vector3(WallWarnerPoints[i % WallWarnerPoints.Count].x, 0.0f, WallWarnerPoints[i % WallWarnerPoints.Count].y));
                Vector3 vec2 = transform.localToWorldMatrix * (new Vector3(WallWarnerPoints[(i + 1) % WallWarnerPoints.Count].x, 0.0f, WallWarnerPoints[(i + 1) % WallWarnerPoints.Count].y));
                Debug.DrawLine(vec1, vec2, Color.blue,0.0f,false);
            }


        }
    }

}


