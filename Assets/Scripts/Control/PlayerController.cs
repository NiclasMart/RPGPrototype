using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    Mover mover;
    Fighter fighter;
    Camera cam;

    private void Start()
    {
      mover = GetComponent<Mover>();
      fighter = GetComponent<Fighter>();
      cam = Camera.main;
    }

    private void Update()
    {
      if (UpdateCombat()) return;
      if (UpdateMovement()) return;
      print("Nothing to do!");
    }

    private bool UpdateCombat()
    {
      Attackable combatTarget = CheckForCombatTarget();

      if (combatTarget)
      {
        if (Input.GetMouseButtonDown(0)) fighter.SetCombatTarget(combatTarget);
        return true;
      }
      return false;
    }

    private Attackable CheckForCombatTarget()
    {
      RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
      foreach (RaycastHit hit in hits)
      {
        Attackable target = hit.transform.GetComponent<Attackable>();
        if (target) return target;
        else continue;
      }
      return null;
    }

    private bool UpdateMovement()
    {
      RaycastHit hit;
      bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

      if (hasHit)
      {
        if (Input.GetMouseButtonDown(0)) mover.StartMoveAction(hit.point);
        return true;
      }
      return false;
    }

    private Ray GetMouseRay()
    {
      return cam.ScreenPointToRay(Input.mousePosition);
    }
  }
}
