using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mover : MonoBehaviour
{
  Camera cam;
  NavMeshAgent agent;

  Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    cam = Camera.main;
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    if (Input.GetMouseButton(0))
    {
      CalculateNextMovementTarget();
    }
    SetAnimation();
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

  private void SetAnimation()
  {
    float currentSpeed = agent.velocity.magnitude;
    Debug.Log(currentSpeed);
    animator.SetFloat("ForwardSpeed", currentSpeed);
  }
}
