using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
  public class Mover : MonoBehaviour
  {

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
      agent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
    }

    private void Update()
    {
      UpdateAnimation();
    }

    public void MoveTo(Vector3 destination)
    {
      agent.destination = destination;
    }

    private void UpdateAnimation()
    {
      float currentSpeed = agent.velocity.magnitude;
      animator.SetFloat("ForwardSpeed", currentSpeed);
    }
  }
}
