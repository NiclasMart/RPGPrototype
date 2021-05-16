using System.Collections;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler), typeof(Mover))]
  public class Attacker : MonoBehaviour, IAction
  {
    protected Mover mover;
    protected Health target;
    protected ActionScheduler scheduler;
    protected Animator animator;
    protected LayerMask collisionLayer;
    [HideInInspector] public bool currentlyAttacking = false;
    public bool HasTarget => target != null;

    protected virtual void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();
      mover = GetComponent<Mover>();
    }

    void Update()
    {
      if (target == null) return;

      if (target.IsDead && !currentlyAttacking) Cancel();
      if (target && !currentlyAttacking) Attack();
    }

    public void SetCombatTarget(GameObject combatTarget, LayerMask layer)
    {
      collisionLayer = layer;
      scheduler.StartAction(this);
      target = combatTarget.GetComponent<Health>();
    }

    public virtual void Attack() { }

    protected void StopAttacking()
    {
      animator.SetTrigger("cancelAttack");
    }

    protected void AdjustAttackDirection()
    {
      transform.LookAt(target.transform, Vector3.up);
    }

    public virtual void Cancel()
    {
      StopAttacking();
      mover.Cancel();
      target = null;
    }
  }
}