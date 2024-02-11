using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weaponPrefab;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }
            print("PICKUP " + name + " TRIGGERED");
            other.GetComponent<Fighter>().EquipWeapon(weaponPrefab);
            Destroy(gameObject);
        }
    }

}