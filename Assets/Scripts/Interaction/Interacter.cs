using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Interaction
{
  public class Interacter : MonoBehaviour, IAction
  {
    [SerializeField] float interactionDistance = 1f;
    Targetable interactionTarget = null;

    ActionScheduler scheduler;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      mover = GetComponent<Mover>();
    }

    private void Update()
    {
      if (!interactionTarget) return;

      if (TargetInRange())
      {
        mover.Cancel();
        interactionTarget.GetComponent<Pickup>().Take(gameObject);
      }
      else
      {
        mover.MoveTo(interactionTarget.transform.position);
      }
    }

    public void SetInteractionTarget(Targetable target)
    {
      scheduler.StartAction(this);
      interactionTarget = target;
    }

    private bool TargetInRange()
    {
      return Vector3.Distance(transform.position, interactionTarget.transform.position) <= interactionDistance;
    }
    public void Cancel()
    {
      interactionTarget = null;
    }
  }
}