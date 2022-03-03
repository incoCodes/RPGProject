
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;


namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            //print("Nothing");

        }

        public bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMosueRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }

            return false;
        }

        public bool InteractWithMovement()
        {
            
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMosueRay(), out hit);

            if (hasHit == true)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                 //    print("Walking");
                }
                return true;
            }
            return false;
        }

        private static Ray GetMosueRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
   





}
