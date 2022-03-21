
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
           
        }
        // Takes all hits in a RayCastHit array made by the mouse clicks and checks if the hit was a combat target, returns true or false
        public bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMosueRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);

                }
                return true;
            }

            return false;
        }

        // Takes a RayCastHit and checks if RayCast has hit anything and moves toward hit point, returns true or false 

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
        // Takes mouse inputs of mouse positions and shoots out a ray from the camera
        private static Ray GetMosueRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
   





}
