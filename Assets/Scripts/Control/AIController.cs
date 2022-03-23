using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDist = 5f;
      

        Fighter fighter;
        GameObject player;
        Health health;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

            
        }
        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
            return distanceToPlayer < chaseDist;
        }
    }
}