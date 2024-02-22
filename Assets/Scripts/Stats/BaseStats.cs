using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null) experience.onExperienceGained += UpdateLevel;
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > startingLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }



        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CurrentLevel);
        }

        public int CurrentLevel 
        {
            get 
            { 
                if(currentLevel < 1)
                {
                    CalculateLevel();
                }
                return currentLevel; 
            } 
        }

        public int CalculateLevel()
        {

            Experience experience = GetComponent<Experience>();
            if(experience == null) return startingLevel;
            float currentXP = experience.ExperiencePoints;
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (xpToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }
    }
}
