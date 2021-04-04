using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] protected float attackRange = 1f;

    protected Transform target;
    protected ActionScheduler scheduler;
    protected Animator animator;

    protected virtual void Start()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
      if (target) Attack();
    }

    protected void PlayAttackAnimation()
    {
      animator.SetBool("Attack", true);
    }

    protected bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < attackRange;
    }

    public void SetCombatTarget(Attackable combatTarget)
    {
      scheduler.StartAction(this);
      target = combatTarget.transform;
    }

    protected virtual void Attack()
    {
      if (TargetInRange())
      {
        PlayAttackAnimation();
        print("I will take all of your LOOT! YOU dumbass " + target.name);
      }
    }

    public void Cancel()
    {
      animator.SetBool("Attack", false);
      target = null;
    }
  }
}