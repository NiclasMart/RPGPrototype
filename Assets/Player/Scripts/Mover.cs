using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mover : MonoBehaviour
{
  [SerializeField] Transform target;

  Camera cam;
  NavMeshAgent agent;

  // Start is called before the first frame update
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    cam = Camera.main;
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      CalculateNextMovementTarget();
    }
  }

  private void CalculateNextMovementTarget()
  {
    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
      agent.destination = hit.point;
    }
  }
}
