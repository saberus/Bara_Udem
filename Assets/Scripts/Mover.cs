using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Ray lastRay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursore();
        }
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100);


        //gameObject.GetComponent<NavMeshAgent>().destination = target.position;
    }

    private void MoveToCursore()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, 1000);
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
