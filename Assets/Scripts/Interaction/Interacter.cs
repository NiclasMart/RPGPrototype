using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Interaction
{
  public class Interacter : MonoBehaviour, IAction
  {
    [SerializeField] float interactionDistance = 1f;
    Inventory inventory;
    IInteraction interactionTarget = null;

    ActionScheduler scheduler;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      mover = GetComponent<Mover>();
      inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
      if (interactionTarget == null) return;

      if (TargetInRange())
      {
        mover.Cancel();
        print("call interact");
        interactionTarget.Interact(gameObject);
        Cancel();
      }
      else
      {
        mover.MoveTo(interactionTarget.GetGameObject().transform.position);
      }
    }

    public void SetInteractionTarget(IInteraction target)
    {
      scheduler.StartAction(this);
      interactionTarget = target;
    }

    private void Interact()
    {
      // Pickup pickup = interactionTarget.GetGameObject().GetComponent<Pickup>();

      // if (inventory.CheckCapacity(pickup.item.weight))
      // {
      //   inventory.AddItem(pickup.item);
      //   pickup.Take();
      // }

    }

    private bool TargetInRange()
    {
      return Vector3.Distance(transform.position, interactionTarget.GetGameObject().transform.position) <= interactionDistance;
    }
    public void Cancel()
    {
      interactionTarget = null;
    }
  }
}