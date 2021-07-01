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
    [SerializeField] int collisionLayer;
    [SerializeField] EnemyHUD hudManager;
    Mover mover;
    PlayerFighter fighter;
    Interacter interacter;
    Health health;
    PlayerCursor playerCursor;
    AbilityManager abilityManager;
    Animator animator;
    ActionScheduler scheduler;

    private void Awake()
    {
      mover = GetComponent<Mover>();
      fighter = GetComponent<PlayerFighter>();
      interacter = GetComponent<Interacter>();
      health = GetComponent<Health>();
      playerCursor = GetComponent<PlayerCursor>();
      abilityManager = GetComponent<AbilityManager>();
      animator = GetComponent<Animator>();
      scheduler = GetComponent<ActionScheduler>();
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
    }

    private bool HandleInputs()
    {
      foreach (KeyCode key in abilityManager.KeySet)
      {
        if (Input.GetKeyDown(key))
        {
          abilityManager.CastAbility(key, collisionLayer);
          return true;
        }
      }
      return false;
    }

    private bool UpdateCombat()
    {
      if (Input.GetMouseButton(1))
      {
        mover.AdjustDirection(playerCursor.Position);
        fighter.Attack(scheduler, playerCursor);

        return true;
      }
      return false;
    }

    private bool UpdateInteraction()
    {
      if (!playerCursor.hasRaycastHit) return false;
      
      Interactable interactionTarget = null;
      IInteraction target = playerCursor.Target;
      if (target != null) interactionTarget = target.GetGameObject().GetComponent<Interactable>();

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
      Debug.Log("SetMovement");
      playerCursor.SetCursor(PlayerCursor.CursorType.STANDARD);
      return true;
    }
  }
}
