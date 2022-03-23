
using UnityEngine;
using UnityEngine.AI;
// Using action Schdueler 
using RPG.Core;
// Created a namespace 
namespace RPG.Movement

{// Goes through the action schdueler 
    public class Mover : MonoBehaviour , IAction
    {
        Health health;
        // Variables for AI
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }
        // Calls MoveTo method through the action schdueler 
        public void StartMoveAction (Vector3 destination)
        {
            GetComponent<ActionSchdueler>().StartAction(this); 
            MoveTo(destination);
        }

        // AI moves towards destination hit by the mouse ray cast
        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
            navMeshAgent.isStopped = false; 
        }
        // Cancels AI movement instantly  
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        
        // Updates animaton based on the speed of the player 
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelo = transform.InverseTransformDirection(velocity);
            float speed = localVelo.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

    }




}
