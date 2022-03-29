using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float circleRad = 0.43f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPoint(i), circleRad);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }




              int GetNextIndex(int i)
            {
                if (i + 1 >= transform.childCount)
                {
                    return 0;
                }
                return i + 1;

            }



            Vector3 GetWayPoint(int i)
            {
                return transform.GetChild(i).position;
            }
        }
        
    }
}
