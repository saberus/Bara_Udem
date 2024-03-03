using RPG.Attributes;
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
        [SerializeField] float healthToRestore = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }
            //print("PICKUP " + name + " TRIGGERED");
            Pickup(other.gameObject);
        }

        private void Pickup(GameObject subject)
        {
            if(weaponPrefab != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weaponPrefab);
            }
            if(healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore); 
            }
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
                Pickup(callingController.gameObject);
            }
            return true;

        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }

}