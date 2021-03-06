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
        [SerializeField] float dwellTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        float wayPointTolerance = 3f;
      

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSiceLastSawPlayer = Mathf.Infinity;
        float timeSinceStoppedWayPoint = Mathf.Infinity;
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
            TimerUpdate();
        }

        private void TimerUpdate()
        {
            timeSiceLastSawPlayer += Time.deltaTime;
            timeSinceStoppedWayPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                        CycleWayPoint();
                    timeSinceStoppedWayPoint = 0;

                }
                nextPosition = GetCurrentWayPoint();
            }
            if (timeSinceStoppedWayPoint > dwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
            
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
            timeSiceLastSawPlayer = 0;
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