using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDist = 5f;

        private void Update()
        {
            if (DistanceToPlayer() <= chaseDist)
            {
                Debug.Log(this.gameObject + "should chase");
            }
        }

        private float DistanceToPlayer()
        {
                GameObject player = GameObject.FindWithTag("Player");
                return Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        }
    }
}