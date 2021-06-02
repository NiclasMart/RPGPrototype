using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Interaction
{
  public class Interacter : MonoBehaviour, IAction
  {
    [SerializeField] float interactionDistance = 1f;
    PlayerInventory inventory;
    Targetable interactionTarget = null;

    ActionScheduler scheduler;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      mover = GetComponent<Mover>();
      inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
      if (!interactionTarget) return;

      if (TargetInRange())
      {
        mover.Cancel();
        Pickup item = interactionTarget.GetComponent<Pickup>();//.Take(gameObject);
        inventory.AddItem(item);
        item.Take();
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