using Aztlan.Combat;
using Aztlan.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; //What distance they should attack 
        Fighter fighter;
        Health health;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player.gameObject);
            }
            else
            {
                GetComponent<Fighter>().Cancel();
            }
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