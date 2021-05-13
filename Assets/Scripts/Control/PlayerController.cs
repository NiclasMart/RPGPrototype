using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Stats;
using RPG.Display;
using RPG.Core;
using RPG.Interaction;
using System;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField] EnemyHUD hudManager;
    Mover mover;
    Fighter fighter;
    Interacter interacter;
    Health health;
    PlayerCursor playerCursor;
    AbilityManager abilityManager;

    private void Awake()
    {
      mover = GetComponent<Mover>();
      fighter = GetComponent<Fighter>();
      interacter = GetComponent<Interacter>();
      health = GetComponent<Health>();
      playerCursor = GetComponent<PlayerCursor>();
      abilityManager = GetComponent<AbilityManager>();
    }

    private void LateUpdate()
    {
      if (health.IsDead)
      {
        playerCursor.SetCursor(PlayerCursor.CursorType.STANDARD);
        playerCursor.active = false;
        return;
      }
      if (HandleInputs()) return;
      if (UpdateCombat()) return;
      if (UpdateInteraction()) return;
      if (UpdateMovement()) return;
      print("Nothing to do!");
    }

    private bool HandleInputs()
    {
      foreach (KeyCode key in abilityManager.KeySet)
      {
        if (Input.GetKeyDown(key))
        {
          abilityManager.CastAbility(key);
          return true;
        }
      }
      return false;
    }

    Health lastDisplayTarget, lastCombatTarget;
    private bool UpdateCombat()
    {
      Health combatTarget = null;
      Targetable target = playerCursor.Target;
      if (target) combatTarget = target.GetComponent<Health>();


      if (combatTarget)
      {
        if (Input.GetMouseButtonDown(0))
        {
          fighter.SetCombatTarget(combatTarget.gameObject);
          lastCombatTarget = combatTarget;
        }

        if (combatTarget != lastDisplayTarget)
        {
          hudManager.SetUpEnemyDisplay(combatTarget, combatTarget.GetComponent<CharacterStats>());
          lastDisplayTarget = combatTarget;
        }
        playerCursor.SetCursor(PlayerCursor.CursorType.COMBAT);
        return true;
      }

      if (lastCombatTarget && lastCombatTarget.IsDead)
      {
        if (SearchForNewTarget()) return true;
      }

      if (!fighter.HasTarget) hudManager.SetUpEnemyDisplay(null, null);
      lastDisplayTarget = combatTarget;
      return false;
    }

    bool SearchForNewTarget()
    {
      Collider[] hits = Physics.OverlapSphere(transform.position, 1f);
      foreach (Collider hit in hits)
      {
        if (hit.transform.CompareTag("Enemy"))
        {
          fighter.SetCombatTarget(hit.gameObject);
          lastCombatTarget = hit.GetComponent<Health>();
          hudManager.SetUpEnemyDisplay(lastCombatTarget, hit.GetComponent<CharacterStats>());
          return true;
        }
      }
      lastCombatTarget = null;
      return false;
    }

    private bool UpdateInteraction()
    {
      Pickup interactionTarget = null;
      Targetable target = playerCursor.Target;
      if (target) interactionTarget = target.GetComponent<Pickup>();

      if (interactionTarget)
      {
        if (Input.GetMouseButtonDown(0)) interacter.SetInteractionTarget(target);
        playerCursor.SetCursor(PlayerCursor.CursorType.INTERACT);
        return true;
      }
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
      playerCursor.SetCursor(PlayerCursor.CursorType.STANDARD);
      return true;
    }
  }
}
