using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField]
        Transform target;

        public Vector3 offset;

        // Delayed update to give camera smoother movement added offset to adjust camera XYZ
        void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
