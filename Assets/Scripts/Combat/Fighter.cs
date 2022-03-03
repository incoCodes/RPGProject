
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

        

        private void Update()
        {
            
            if (target == null) return;
            
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
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget comTarget)
        {
            GetComponent<ActionSchdueler>().StartAction(this);
            target = comTarget.transform;            
            
        } 

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
