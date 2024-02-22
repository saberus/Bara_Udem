using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health health = fighter.Target;
            string healthStringValue = "N/A";
            if(health != null)
            {
                healthStringValue = string.Format("{0:0}/{1:0}", health.HealthPoints.ToString(), health.GetMaxHealthPoints());
            }
            GetComponent<Text>().text = healthStringValue;
        }
    }
}
