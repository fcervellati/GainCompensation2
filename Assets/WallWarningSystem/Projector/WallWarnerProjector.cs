/***********************************************************************************************************
 * 
 * Author: Markus Zank
 * Last change: 16.02.16
 * Creates a wallwarner using a projector
 * 
***********************************************************************************************************/

using UnityEngine;

[RequireComponent(typeof(Projector))]
public class WallWarnerProjector : MonoBehaviour {
    
    //reference to the image
    Texture2D image;

    //pixel size of the texture
    public int textureSize;

    //width of the line in pixels
    public int lineWidth;

    //number of pixels between the line and the border of the image
    public int border;
    //color of the line
    public Color color;

	void Start () {
        
        Settings.local.AddOnChangeWallWarner(CreateWallWarner);
        
    }

    void OnDestroy()
    {
        Settings.local.RemoveOnChangeWallWarner(CreateWallWarner);
    }

    public void CreateWallWarner(Vector2[] points)
    {
        //get Projector reference
        Projector projector = GetComponent<Projector>();

        //Create new Texture2D
        image = new Texture2D(textureSize, textureSize);

        //find maximum dimensions
        float minX = Mathf.Infinity;
        float minY = Mathf.Infinity;
        float maxX = -Mathf.Infinity;
        float maxY = -Mathf.Infinity;
        for (int i = 0; i < points.Length; i++)
        {
            minX = Mathf.Min(minX, points[i].x);
            minY = Mathf.Min(minY, points[i].y);
            maxX = Mathf.Max(maxX, points[i].x);
            maxY = Mathf.Max(maxY, points[i].y);
        }
        

        //get longest dimension
        float longestDimension = Mathf.Max(maxX - minX, maxY - minY);

        
        //calculate usable space to keep borders
        int usableSpace = textureSize - 2 * (border + lineWidth / 2);

        //Calculate scaling
        float frac = usableSpace / longestDimension;
        //get offset
        Vector2 offset = new Vector2(minX * frac - border - lineWidth / 2, minY * frac - border - lineWidth / 2);

        //calculate the projectors local position
        transform.localPosition = new Vector3((offset.x+textureSize / 2+lineWidth-border)/frac, 5, (offset.y+textureSize / 2 -lineWidth+ border) / frac);
                
        projector.orthographicSize = longestDimension*  textureSize / (2* usableSpace);


        //translate to pixel coordinates
        Vector2[] pixelPoints = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            pixelPoints[i] = new Vector2(points[i].x * frac - offset.x, points[i].y * frac - offset.y);
            Debug.Log(pixelPoints[i]);
        }


        //iterate over points
        for (int p = 0; p < pixelPoints.Length; p++)
        {
            Vector2 delta = pixelPoints[(p + 1)%pixelPoints.Length] - pixelPoints[p];

            //iterate over pixels
            for (int x = 0; x < textureSize; x++)
            {
                for (int y = 0; y < textureSize; y++)
                {
                    //check if pixel is within lineWidth/2
                    Vector2 offDelta = new Vector2(x, y) - pixelPoints[p];

                    Vector3 dist = Vector3.ProjectOnPlane(offDelta, delta);
                    Vector3 along = Vector3.Project(offDelta, delta);

                    if (dist.magnitude<lineWidth/2 && Vector3.Dot(delta, along) >0 && delta.magnitude>= along.magnitude)
                    {
                        image.SetPixel(x, y, Color.red);
                    }
                        

                }
            }
        }

        //set mode to clamp, otherwise it will repeate outside
        image.wrapMode = TextureWrapMode.Clamp;

        //apply changes to texture
        image.Apply();

        //set new texture to projector
        projector.material.SetTexture("_ShadowTex", image);

    }
}
