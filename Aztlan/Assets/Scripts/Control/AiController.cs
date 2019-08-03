using Aztlan.Combat;
using Aztlan.Core;
using Aztlan.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; //What distance they should attack 
        [SerializeField] float suspictionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;

        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = .2f;

        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceSawPlayer = Mathf.Infinity;
        float timeSinceArriveAtWaypoint = Mathf.Infinity;

        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
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
            else if (timeSinceSawPlayer <= suspictionTime && timeSinceSawPlayer >= 0)
            {
                //Suspiction state
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimes();
        }

        private void UpdateTimes()
        {
            timeSinceSawPlayer += Time.deltaTime;
            timeSinceArriveAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;


            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArriveAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if(timeSinceArriveAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition,patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            //currentWaypointIndex
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }



        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceSawPlayer = 0;
            fighter.Attack(player.gameObject);
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(player.transform.position, gameObject.transform.position) <= chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(gameObject.transform.position, chaseDistance);
        }
    }
}