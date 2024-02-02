﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Core
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
                print(currentAction + " canceled");
            } 

            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }

    }
}