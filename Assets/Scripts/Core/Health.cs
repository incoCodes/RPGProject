using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
       [SerializeField] float healthPoints = 100f;
        bool isDead = false;

     
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints -= damage, 0);
            Debug.Log(healthPoints);

           if (healthPoints == 0) Die();
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<ActionSchdueler>().CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}