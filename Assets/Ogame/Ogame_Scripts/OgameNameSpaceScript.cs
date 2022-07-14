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
                float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

                return deg;
            }
            public static float[] Direction(float rd)
            {
                float[] xy = new float[2];

                if (rd >= -45f & rd < 45f)
                {
                    xy[0] = 1f;//right
                    xy[1] = 0;
                    return xy;
                }
                else if (rd >= 45f & rd < 135f)
                {
                    xy[0] = 0;//back
                    xy[1] = 1f;
                    return xy;
                }
                else if ((rd >= 135f & rd < 180f) | (rd < -135f & rd >= -180f))
                {
                    xy[0] = -1f;//left
                    xy[1] = 0;
                    return xy;
                }
                else if (rd >= -135f & rd < -45f)
                {
                    xy[0] = 0;//front
                    xy[1] = -1f;
                    return xy;
                }

                return xy;
            }
        }
    }
}