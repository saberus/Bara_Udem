using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regegenerationPercentage = 70;
        [SerializeField] UnityEvent<float> takeDamage;
        [SerializeField] UnityEvent onDie;

        LazyValue<float> healthPoints;

        LazyValue<float> maxHealthPoints;

        bool isDead = false;

        BaseStats baseStats = null;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = new LazyValue<float>(GetInitialHealth);
            maxHealthPoints = new LazyValue<float>(GetMaxHealthPoints);
        }

        private float GetInitialHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
            maxHealthPoints.ForceInit();
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }

        public bool IsDead ()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //print(name + " taking " + damage + " damage");
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            
            //print(healthPoints);
            CheckHealth();
            if (isDead)
            {
                onDie.Invoke();
                AwardExperience(instigator);
            } else
            {
                takeDamage.Invoke(damage);
            }
        }
        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (LazyValue<float>)state;
            CheckHealth();
        }
        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regegenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);  
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            float xp = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            instigator.GetComponent<Experience>().GainExperience(xp);
            
        }

        public float HealthPoints { get { return healthPoints.value; } }

        public float GetMaxHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetHealthFraction();
        }

        public float GetHealthFraction()
        {
            return healthPoints.value / maxHealthPoints.value;
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetMaxHealthPoints());
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
            if (healthPoints.value == 0)
            {
                Die();
            }
        }

    }

}