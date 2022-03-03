
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
namespace RPG.Movement

{
    public class Mover : MonoBehaviour , IAction
    {
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction (Vector3 destination)
        {
            GetComponent<ActionSchdueler>().StartAction(this); 
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
            navMeshAgent.isStopped = false; 
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelo = transform.InverseTransformDirection(velocity);
            float speed = localVelo.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

    }




}
