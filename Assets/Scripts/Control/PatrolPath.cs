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

                Gizmos.DrawSphere(transform.GetChild(i).position, circleRad);
            }
        }
    }
}
