using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{

    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneIndex = -1;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
