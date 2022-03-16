
using UnityEngine;

namespace RPG.Combat
{
    // Adds a health script to the object if object has the CombatTarget Script, CombatTarget requires health 
    [RequireComponent(typeof(Health))]   
    public class CombatTarget : MonoBehaviour
    {

    }
   
}
