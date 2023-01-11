using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMovement : MonoBehaviour
{
    public Camera camera;

    private NavMeshAgent agent;
    private RaycastHit hit;

    private string tagGround = "Ground";

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool leftClick = Input.GetMouseButtonDown(0);

        if (leftClick)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(tagGround)) agent.SetDestination(hit.point);
            }
        }
    }
}
