using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
  [RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(ActionScheduler))]
  public class Mover : MonoBehaviour, IAction
  {

    NavMeshAgent agent;
    Animator animator;
    ActionScheduler scheduler;

    void Start()
    {
      agent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
      scheduler = GetComponent<ActionScheduler>();
    }

    private void Update()
    {
      UpdateAnimation();
    }

    public void StartMoveAction(Vector3 destination)
    {
      scheduler.StartAction(this);
      MoveTo(destination);
    }

    public void MoveTo(Vector3 destination)
    {
      agent.destination = destination;
    }

    public void Cancel()
    {
      agent.destination = transform.position;
    }

    private void UpdateAnimation()
    {
      float currentSpeed = agent.velocity.magnitude;
      animator.SetFloat("forwardSpeed", currentSpeed);
    }
  }
}
