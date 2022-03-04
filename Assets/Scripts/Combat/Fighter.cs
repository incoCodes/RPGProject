
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        Transform target;

      

        [SerializeField]
        float weaponRange = 2f;

        [SerializeField]
        float timeBetweenAttacks = 1f;

        float timeSinceLastAttack = 0;

        

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            // Null check for target
            if (target == null) return;
            // Checks to see if enemy was within attack range and moves towards enemy if not and cancels movement and calls attack animation if within range
            if (!GetRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }

            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
               
            }


        }
        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // Triggers attack animation if attack time cooldown is less than last attack timer, resets timer once animation is triggered

                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
           
        }
        // Gets the attak distance between player and the enemy positon and checks if its less that the attack range of player
        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
        // sets target as a combat target and attack method goes through action schdueler as current action
        public void Attack(CombatTarget comTarget)
        {
            GetComponent<ActionSchdueler>().StartAction(this);
            target = comTarget.transform;            
            
        } 
        // sets target as null in order to cancel 
        public void Cancel()
        {
            target = null;
        }    

        // Animation Event 
        void Hit ()
        {

        }

      
    }
}
