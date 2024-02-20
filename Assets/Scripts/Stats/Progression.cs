using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        //private void Start()
        //{
        //    BuildLookup();
        //}

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            //foreach (ProgressionCharacterClass progressionCharacter in characterClasses)
            //{
            //    if (progressionCharacter.characterClass != characterClass) continue;

            //    foreach(ProgressionStat progressionStat in progressionCharacter.stats)
            //    {
            //        if (progressionStat.stat != stat) continue;
            //        if(progressionStat.levels.Length < level) continue;
            //        return progressionStat.levels[level - 1];
            //    }
            //}
            //return 0;
            //TODO: IS IT GOOD?
            //Dictionary<Stat, float[]> statsDictionary = null;
            //float[] levels= null;
            //lookupTable.TryGetValue(characterClass, out statsDictionary);
            //if(statsDictionary == null) return 0;
            //statsDictionary.TryGetValue(stat, out levels);
            //if(levels.Length < level) return 0;
            //return levels[level];

            float[] levels = lookupTable[characterClass][stat];
            if (levels.Length < level) return 0;
            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionCharacter in characterClasses)
            {
                Dictionary<Stat, float[]> statLookupTable = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionCharacter.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progressionCharacter.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }

}