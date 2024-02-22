using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        //[SerializeField] Text playerHealthValue = null;
        //[SerializeField] Text enemyHealthValue = null;

        Health health;
        //Fighter fighter;
        

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            //fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0:0}/{1:0}", health.HealthPoints.ToString(), health.GetMaxHealthPoints());
            
        }
    }
}
