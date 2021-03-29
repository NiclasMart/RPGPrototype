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
      UpdateCombat();
      UpdateMovement();
    }

    private void UpdateCombat()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Attackable combatTarget = CheckForCombatTarget();
        if (combatTarget) fighter.Attack(combatTarget);
      }
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

    private void UpdateMovement()
    {
      if (Input.GetMouseButton(0))
      {
        CalculateNextMovementPosition();
      }
    }

    private void CalculateNextMovementPosition()
    {
      RaycastHit hit;
      if (Physics.Raycast(GetMouseRay(), out hit))
      {
        mover.MoveTo(hit.point);
      }
    }

    private Ray GetMouseRay()
    {
      return cam.ScreenPointToRay(Input.mousePosition);
    }
  }
}
