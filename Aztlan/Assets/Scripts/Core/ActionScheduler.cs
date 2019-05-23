using Aztlan.Movement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aztlan.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        
        private IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }                
            currentAction = action;
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}