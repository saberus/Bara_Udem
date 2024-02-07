using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        //IEnumerator FadeOutIn()
        //{
        //    yield return FadeOut(2f);
        //    print("Faded Out");
        //    yield return FadeIn(1f);
        //    print("Faded In");
        //}

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;//will update with the next frame
            }
            
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;//will update with the next frame
            }

        }
    }
}
