using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Stats;
using RPG.Display;
using RPG.Core;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] EnemyHUD hudManager;
    Mover mover;
    Fighter fighter;
    Health health;
    Camera cam;
    PlayerCursor playerCursor;

    private void Awake()
    {
      mover = GetComponent<Mover>();
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      playerCursor = GetComponent<PlayerCursor>();
    }

    private void Start()
    {
      cam = PlayerInfo.GetMainCamera();
    }

    private void LateUpdate()
    {
      if (health.IsDead) return;

      if (UpdateCombat()) return;
      if (UpdateMovement()) return;
      print("Nothing to do!");
    }

    Health lastTarget;
    private bool UpdateCombat()
    {
      Health combatTarget = null;
      Targetable target = playerCursor.Target;
      if (target) combatTarget = target.GetComponent<Health>();


      if (combatTarget && !combatTarget.IsDead)
      {
        if (Input.GetMouseButtonDown(0)) fighter.SetCombatTarget(combatTarget.gameObject);

        if (combatTarget != lastTarget)
        {
          hudManager.SetUpEnemyDisplay(combatTarget, combatTarget.GetComponent<CharacterStats>());
          lastTarget = combatTarget;
        }
        playerCursor.SetCursor(PlayerCursor.CursorType.COMBAT);
        return true;
      }

      if (!fighter.HasTarget) hudManager.SetUpEnemyDisplay(null, null);
      lastTarget = combatTarget;
      return false;


    }

    private bool UpdateMovement()
    {
      if (!playerCursor.hasRaycastHit) return false;

      Vector3 movePosition = playerCursor.Position;
      if (Input.GetMouseButton(0))
      {
        mover.StartMoveAction(movePosition);
        hudManager.SetUpEnemyDisplay(null, null);
      }
      playerCursor.SetCursor(PlayerCursor.CursorType.MOVE);
      return true;
    }

    private Ray GetMouseRay()
    {
      return cam.ScreenPointToRay(Input.mousePosition);
    }
  }
}
