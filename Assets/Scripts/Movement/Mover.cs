using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
  [RequireComponent(typeof(NavMeshAgent), typeof(ActionScheduler))]
  public class Mover : MonoBehaviour, IAction
  {

    NavMeshAgent agent;
    Animator animator;
    ActionScheduler scheduler;

    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
      animator = GetComponentInChildren<Animator>();
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

    public void SetMovementSpeed(float speed)
    {
      agent.speed = Mathf.Abs(speed);
    }

    public bool ReachedDestination()
    {
      if (agent.remainingDistance == 0) return true;
      else return false;
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
