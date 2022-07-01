using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class OgameNameSpaceScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/

namespace Ogame
{
    namespace System
    {
        public class ClassA
        {
            public static float FindToTargetDeg(string s, Vector2 t)
            {
                Vector2 p1 = t;
                Vector2 p2 = GameObject.Find(s).transform.position;
                Vector2 dt = p2 - p1;
                float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg * (-1) - 90f;

                return deg;
            }
        }
    }
}