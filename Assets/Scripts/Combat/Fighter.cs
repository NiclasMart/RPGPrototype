using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] protected Transform rightWeaponHolder;
    [SerializeField] protected Weapon defaultWeapon;
    [SerializeField] protected Weapon weapon = null;


    protected Transform target;
    protected ActionScheduler scheduler;
    protected Animator animator;

    protected virtual void Start()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();

      EquipWeapon();
    }

    protected virtual void Update()
    {
      if (target) Attack();
    }

    public void SetCombatTarget(GameObject combatTarget)
    {
      scheduler.StartAction(this);
      target = combatTarget.transform;
      animator.ResetTrigger("cancelAttack");
    }

    public virtual void Cancel()
    {
      animator.SetTrigger("cancelAttack");
      target = null;

    }

    protected virtual void Attack()
    {
      if (TargetInRange())
      {
        PlayAttackAnimation();
        AdjustAttackDirection();
        /*damage is dealt by the animation Hit() event*/
      }
    }

    protected bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < weapon.AttackRange;
    }

    protected void AdjustAttackDirection()
    {
      transform.LookAt(target, Vector3.up);
    }

    float attackState = 0f;
    float lastAttackTime = -Mathf.Infinity;
    protected void PlayAttackAnimation()
    {
      if (lastAttackTime + 1 / weapon.AttackSpeed <= Time.time)
      {
        //switch between thw different attack animations
        animator.SetFloat("attackState", attackState);
        attackState = (++attackState % 2);
        //set up animation
        animator.SetTrigger("attack");
        lastAttackTime = Time.time;
      }
    }

    private void EquipWeapon()
    {
      Animator animator = GetComponent<Animator>();
      if (weapon == null) weapon = defaultWeapon;
      weapon.Spawn(rightWeaponHolder, animator);
    }

    //animation event (called from animator)
    void Hit()
    {
      if (target)
      {
        bool targetDies = target.GetComponent<Health>().ApplyDamage(weapon.Damage);
        if (targetDies) Cancel();
        print("do damage");
      }
    }
  }
}