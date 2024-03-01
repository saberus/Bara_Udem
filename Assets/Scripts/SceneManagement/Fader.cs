using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        Coroutine currentActiveFade = null;

        private void Awake()
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

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            return Fade(1f, time);
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0f, time);
        }

        public IEnumerator Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));

            yield return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha , target, Time.deltaTime / time); //works in both directions
                yield return null;//will update with the next frame
            }
        }
    }
}
