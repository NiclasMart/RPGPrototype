using UnityEngine;
using RPG.Core;
using RPG.Stats;
using System.Collections;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction, IStatModifier
  {
    [SerializeField] protected Transform rightWeaponHolder;
    [SerializeField] protected Transform leftWeaponHolder;
    [SerializeField] protected Weapon defaultWeapon;

    protected Health target;
    protected ActionScheduler scheduler;
    protected Animator animator;
    protected Weapon currentWeapon = null;

    public bool HasTarget => target != null;

    protected virtual void Start()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();

      EquipWeapon(defaultWeapon);
    }

    protected virtual void Update()
    {
      if (target == null) return;

      if (target.IsDead) Cancel();
      if (target) Attack();
    }

    public void SetCombatTarget(GameObject combatTarget)
    {
      scheduler.StartAction(this);
      target = combatTarget.GetComponent<Health>();
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
      if (lastAttackTime + (1f / currentWeapon.AttackSpeed) <= Time.time)
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
      transform.LookAt(target.transform, Vector3.up);
    }

    GameObject weaponReference;
    public void EquipWeapon(Weapon weapon)
    {
      if (weaponReference) Destroy(weaponReference);
      Animator animator = GetComponent<Animator>();
      weaponReference = weapon.Equip(rightWeaponHolder, leftWeaponHolder, animator);
      currentWeapon = weapon;

    }

    //animation event (called from animator)
    void Hit()
    {
      if (target == null) return;

      float damage = GetComponent<CharacterStats>().GetStat(Stat.DAMAGE);
      if (currentWeapon is RangedWeapon)
      {
        RangedWeapon weapon = (RangedWeapon)currentWeapon;
        weapon.LaunchProjectile(rightWeaponHolder, leftWeaponHolder, target, gameObject, damage);
      }
      else
      {
        target.GetComponent<Health>().ApplyDamage(gameObject, damage);
      }
    }

    //animation event (called from animator)
    void Shoot()
    {
      print(transform.name + " shoots");
      Hit();
    }

    public IEnumerable GetAdditiveModifier(Stat stat)
    {
      if (stat == Stat.DAMAGE)
      {
        yield return currentWeapon.Damage;
      }
    }
  }
}