using RPG.Control;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weaponPrefab;
        [SerializeField] float respawnTime = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }
            //print("PICKUP " + name + " TRIGGERED");
            Pickup(other.GetComponent<Fighter>());
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weaponPrefab);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<SphereCollider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;

        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }

}