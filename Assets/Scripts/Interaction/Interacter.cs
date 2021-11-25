using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Interaction
{
  public class Interacter : MonoBehaviour, IAction
  {
    public SimpleInventory inventory;
    public PlayerInventory mainInventory;
    [SerializeField] float interactionDistance = 1f;

    IInteraction interactionTarget = null;
    ActionScheduler scheduler;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      mover = GetComponent<Mover>();
    }

    private void Update()
    {

      if (interactionTarget == null) return;

      if (TargetInRange())
      {
        mover.Cancel();
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
      if (!scheduler.StartAction(this, false)) return;
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