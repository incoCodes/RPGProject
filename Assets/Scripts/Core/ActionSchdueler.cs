using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class ActionSchdueler : MonoBehaviour
    {
        // Takes variable of type IAction and starts the current action that the player is currently doing while cancelling the previous action 
        // Basically making sure that there is no conflict between the different player states
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

        public void CancelCurrentAction ()
        {
            StartAction(null);
        }
    }
}