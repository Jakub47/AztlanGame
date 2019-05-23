using Aztlan.Combat;
using Aztlan.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Aztlan.Movement
{

    public class Mover : MonoBehaviour , IAction
    {
        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            // dest.destination = target.transform.position;
            //if(Input.GetMouseButton(0))
            //{
            //    MoveToCursor();
            //}
            UpdateAnimator();
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }


        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVeloctity = transform.InverseTransformDirection(velocity);
            float speed = localVeloctity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

    }

}
