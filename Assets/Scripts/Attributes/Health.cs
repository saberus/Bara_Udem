using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regegenerationPercentage = 70;

        float healthPoints = -1f;

        float maxHealthPoints = 0f;

        bool isDead = false;

        BaseStats baseStats = null;

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            float healthFromStats = baseStats.GetStat(Stat.Health);
            baseStats.onLevelUp += RegenerateHealth;
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
        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            CheckHealth();
        }
        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regegenerationPercentage / 100);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);  
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            float xp = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            instigator.GetComponent<Experience>().GainExperience(xp);
            
        }

        public float HealthPoints { get { return healthPoints; } }

        public float GetMaxHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
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



        private void CheckHealth()
        {
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }

}