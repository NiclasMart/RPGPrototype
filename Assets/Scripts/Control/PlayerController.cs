using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Stats;
using RPG.Display;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] EnemyHUD hudManager;
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

    Health lastTarget;
    private bool UpdateCombat()
    {
      Health combatTarget = CheckForCombatTarget();

      if (combatTarget)
      {
        if (Input.GetMouseButtonDown(0)) fighter.SetCombatTarget(combatTarget.gameObject);

        if (combatTarget != lastTarget)
        {
          hudManager.SetUpEnemyDisplay(combatTarget, combatTarget.GetComponent<BaseStats>());
          lastTarget = combatTarget;
        }

        return true;
      }

      if (!fighter.HasTarget) hudManager.SetUpEnemyDisplay(null, null);
      lastTarget = combatTarget;
      return false;

    }

    private Health CheckForCombatTarget()
    {
      RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
      foreach (RaycastHit hit in hits)
      {
        Attackable target = hit.transform.GetComponent<Attackable>();
        if (target && target.CanBeAttacked()) return target.GetComponent<Health>();
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
        if (Input.GetMouseButton(0))
        {
          mover.StartMoveAction(hit.point);
          hudManager.SetUpEnemyDisplay(null, null);
        }
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
