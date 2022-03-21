
using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        Health target;


      

        [SerializeField]
        float weaponRange = 2f;
        [SerializeField]
        float weaponDamage = 5f;

        [SerializeField]
        float timeBetweenAttacks = 1f;

        float timeSinceLastAttack = 0;

        

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            // Null check for target
            if (target == null) return;
            if (target.IsDead()) return;
            // Checks to see if enemy was within attack range and moves towards enemy if not and cancels movement and calls attack animation if within range
            if (!GetRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
                
                
            }

            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
               
            }


        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // Triggers attack animation if attack time cooldown is less than last attack timer, resets timer once animation is triggered
                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("Stopattack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event 
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
           
        }
        // Gets the attak distance between player and the enemy positon and checks if its less that the attack range of player
        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();

            return targetToTest != null && !targetToTest.IsDead();

        }
        // sets target as a combat target and attack method goes through action schdueler as current action
        public void Attack(GameObject comTarget)
        {
            GetComponent<ActionSchdueler>().StartAction(this);
            target = comTarget.GetComponent<Health>();            
            
        } 
        // sets target as null in order to cancel 
        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("Stopattack");
        }





    }
}
