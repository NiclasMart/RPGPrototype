using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] protected Transform rightWeaponHolder;
    [SerializeField] protected Transform leftWeaponHolder;
    [SerializeField] protected Weapon defaultWeapon;

    protected Transform target;
    protected ActionScheduler scheduler;
    protected Animator animator;

    protected Weapon currentWeapon = null;

    protected virtual void Start()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();

      EquipWeapon(defaultWeapon);
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
      if (CanAttack() && TargetInRange())
      {
        PlayAttackAnimation();
        AdjustAttackDirection();

        /*damage is dealt by the animation Hit() event*/
        /*projectile for Ranged Weapons is launched within the Shoot() event */
      }
    }

    float lastAttackTime = -Mathf.Infinity;
    private bool CanAttack()
    {
      if (lastAttackTime + 1 / currentWeapon.AttackSpeed <= Time.time)
      {
        lastAttackTime = Time.time;
        return true;
      }
      return false;
    }

    protected bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.AttackRange;
    }

    float attackState = 0f;
    protected void PlayAttackAnimation()
    {
      //switch between thw different attack animations
      animator.SetFloat("attackState", attackState);
      attackState = (++attackState % 2);
      //set up animation
      animator.SetTrigger("attack");
    }

    protected void AdjustAttackDirection()
    {
      transform.LookAt(target, Vector3.up);
    }

    public void EquipWeapon(Weapon weapon)
    {
      Animator animator = GetComponent<Animator>();
      currentWeapon = weapon.Equip(rightWeaponHolder, leftWeaponHolder, animator);

    }

    //animation event (called from animator)
    void Hit()
    {
      if (target)
      {
        bool targetDies = target.GetComponent<Health>().ApplyDamage(currentWeapon.Damage);
        if (targetDies) Cancel();
        print("do damage");
      }
    }

    //animation event (called from animator)
    void Shoot()
    {
      if (currentWeapon is RangedWeapon)
      {
        RangedWeapon weapon = (RangedWeapon)currentWeapon;
        weapon.LaunchProjectile(target);
      }
    }
  }
}