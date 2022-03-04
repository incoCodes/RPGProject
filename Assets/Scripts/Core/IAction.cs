using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // All scripts using action schdueler must contain the Cancel method 
    public interface IAction
    {
        void Cancel();
    }
}