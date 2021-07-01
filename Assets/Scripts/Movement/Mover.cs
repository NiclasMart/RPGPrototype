using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Stats;
using RPG.Combat;

namespace RPG.Movement
{
  [RequireComponent(typeof(NavMeshAgent), typeof(ActionScheduler))]
  public class Mover : MonoBehaviour, IAction
  {
    NavMeshAgent agent;
    Animator animator;
    ActionScheduler scheduler;

    public bool IsMoving => agent.destination != null;

    private void Awake()
    {
      agent = GetComponent<NavMeshAgent>();
      animator = GetComponentInChildren<Animator>();
      scheduler = GetComponent<ActionScheduler>();

      GetComponent<CharacterStats>().statsChange += UpdateMovementSpeed;
    }

    private void Start()
    {
      SetMovementSpeed(GetComponent<CharacterStats>().GetStat(Stat.MovementSpeed));
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

    public void AdjustDirection(Vector3 lookPoint)
    {
      lookPoint.y = transform.position.y;
      transform.LookAt(lookPoint);
    }

    public void UpdateMovementSpeed(CharacterStats stats)
    {
      Debug.Log("Update Movementspeed: " + stats.GetStat(Stat.MovementSpeed));
      SetMovementSpeed(stats.GetStat(Stat.MovementSpeed));
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
