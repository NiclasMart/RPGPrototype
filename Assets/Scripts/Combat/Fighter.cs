using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected float timeBetweenAttacks = 1f;

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

    float attackState = 0f;
    float lastAttackTime;
    protected void PlayAttackAnimation(bool shouldPlay)
    {
      if (lastAttackTime + timeBetweenAttacks <= Time.time)
      {
        //switch between thw different attack animations
        animator.SetFloat("attackState", attackState);
        attackState = (++attackState % 2);
        //set up animation
        animator.SetTrigger("attack");
        lastAttackTime = Time.time;
      }
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
      if (TargetInRange()) PlayAttackAnimation(true);
    }

    public void Cancel()
    {
      PlayAttackAnimation(false);
      target = null;
    }

    //animation event (called from)
    void Hit()
    {
      print("do damage");
    }
  }
}