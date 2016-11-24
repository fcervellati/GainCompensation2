using UnityEngine;
using System.Collections;

namespace StartUp
{
    public class SpawnOnStartup : StartUpScript
    {

        public GameObject obj;
        public Vector3 position;
        public Quaternion orientation;

        public string objectName;

        public bool network;

        public override void StartUp()
        {
            GameObject go = null;
            if (network)
            {
                go = Network.Instantiate(obj, position, orientation, 0) as GameObject;
            }
            else
            {
                go = Instantiate(obj, position, orientation) as GameObject;
            }
            if(go!=null && objectName.Length>0)
            {
                go.name = objectName;
            }
        }

    }
}