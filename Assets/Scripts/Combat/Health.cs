using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
       public Animator animator;
       [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health -= damage, 0);
            Debug.Log(health);

            if (health == 0)
            {
                animator.SetTrigger("dead");
            }
        }

    }
}