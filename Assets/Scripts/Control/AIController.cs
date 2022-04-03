using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDist = 5f;
        [SerializeField] float susTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        float wayPointTolerance = 1f;
      

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSiceLastSawPlayer = Mathf.Infinity;
        int currentWayPointIndex = 0;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                timeSiceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSiceLastSawPlayer < susTime)
            {
                SusBehaviour();
            }
            else
            {
                PatrolBehaviour();

            }

            timeSiceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());

            return (distanceToWayPoint < wayPointTolerance);
        }

        private void SusBehaviour()
        {
            GetComponent<ActionSchdueler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
            return distanceToPlayer < chaseDist;
        }
        // Called by Unity 
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDist);
        }
    }
}