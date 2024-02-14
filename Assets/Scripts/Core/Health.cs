using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField]
        float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead ()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            print(name + " taking "+ damage + " damage");
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            print(healthPoints);
            CheckHealth();
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }

}