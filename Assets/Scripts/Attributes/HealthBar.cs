using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image foreground;
        [SerializeField] Health health = null;
        [SerializeField] Canvas rootCanvas = null;

        void Update()
        {
            print("fraction in health bar : " + health.GetHealthFraction());
            //TODO: fix aproximation, cantvas not disabling. NAN for some reason 
            if(Mathf.Approximately(health.GetHealthFraction(), 0)
            || Mathf.Approximately(health.GetHealthFraction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.rectTransform.localScale = new Vector3(Mathf.Min(health.GetHealthFraction(), 1), 1, 1);
        }
    }
}
