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

    public void Attack(Health combatTarget, LayerMask layer)
    {
      collisionLayer = layer;
      scheduler.StartAction(this);
      target = combatTarget;
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

    protected void AdjustAttackDirection(float offset)
    {
      transform.LookAt(target.transform, Vector3.up);
      transform.Rotate(Vector3.up * offset);
    }

    public virtual void Cancel()
    {
      StopAttacking();
      mover.Cancel();
      target = null;
    }
  }
}