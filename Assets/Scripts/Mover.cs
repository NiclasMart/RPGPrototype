using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mover : MonoBehaviour
{
  [SerializeField] Transform target;

  NavMeshAgent agent;
  // Start is called before the first frame update
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    if (agent)
    {
      agent.destination = target.position;
    }
  }
}
