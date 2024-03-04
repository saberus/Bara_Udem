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
            float healthFraction = health.GetHealthFraction();
            if (Mathf.Approximately(healthFraction, 0)
            || Mathf.Approximately(healthFraction, 1))
            {
                rootCanvas.enabled = false;
                return;
            }
            rootCanvas.enabled = true;
            foreground.rectTransform.localScale = new Vector3(Mathf.Min(health.GetHealthFraction(), 1), 1, 1);
        }
    }
}
