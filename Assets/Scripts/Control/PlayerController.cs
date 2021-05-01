using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;


namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    Mover mover;
    Fighter fighter;
    Health health;
    Camera cam;

    private void Start()
    {
      mover = GetComponent<Mover>();
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      cam = Camera.main;
    }

    private void Update()
    {
      if (health.IsDead) return;
      
      if (UpdateCombat()) return;
      if (UpdateMovement()) return;
      print("Nothing to do!");
    }

    private bool UpdateCombat()
    {
      Attackable combatTarget = CheckForCombatTarget();

      if (combatTarget)
      {
        if (Input.GetMouseButtonDown(0)) fighter.SetCombatTarget(combatTarget.gameObject);
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
        if (target && target.CanBeAttacked()) return target;
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
        if (Input.GetMouseButton(0)) mover.StartMoveAction(hit.point);
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
