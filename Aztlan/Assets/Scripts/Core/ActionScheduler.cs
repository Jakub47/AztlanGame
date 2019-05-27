﻿using Aztlan.Movement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aztlan.Core
{
    public class ActionScheduler : MonoBehaviour
    {

        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
    }
}