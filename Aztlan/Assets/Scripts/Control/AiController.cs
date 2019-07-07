using Aztlan.Combat;
using Aztlan.Core;
using Aztlan.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; //What distance they should attack 
        [SerializeField] float suspictionTime = 3f;

        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceSawPlayer = Mathf.Infinity;

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
                timeSinceSawPlayer = 0;
                AttackBehaviour();
            }
            else if(timeSinceSawPlayer <= suspictionTime && timeSinceSawPlayer >= 0)
            {
                //Suspiction state
                SuspiciousBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
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