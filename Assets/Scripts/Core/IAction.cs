using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // All scripts using action schdueler must contain the Cancel method 
    // IAction is used so that we can add the same Cancel method to both the mover and the figher script and pass them through the action schdueler
    public interface IAction
    {
        void Cancel();
    }
}