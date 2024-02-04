using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered;
        private void OnTriggerEnter(Collider other)
        {
            if (alreadyTriggered) { return; }
            if (!other.CompareTag("Player")) { return; }
            GetComponent<PlayableDirector>().Play();
            alreadyTriggered = true;
        }

    }

}