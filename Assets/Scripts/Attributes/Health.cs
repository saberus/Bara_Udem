using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;

        float maxHealthPoints = 0f;

        bool isDead = false;

        private void Start()
        {
            float healthFromStats = GetComponent<BaseStats>().GetStat(Stat.Health);

            if (healthPoints < 0f)
            {
                healthPoints = healthFromStats;
            }
            maxHealthPoints = healthFromStats;
        }

        public bool IsDead ()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(name + " taking " + damage + " damage");
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            print(healthPoints);
            CheckHealth();
            if (isDead)
            {
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            float xp = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            instigator.GetComponent<Experience>().GainExperience(xp);
            
        }

        public float GetPercentage()
        {
            return (healthPoints * 100) / maxHealthPoints;
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