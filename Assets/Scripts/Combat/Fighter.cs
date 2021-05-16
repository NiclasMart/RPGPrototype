using UnityEngine;
using RPG.Core;
using RPG.Stats;
using System.Collections;
using GameDevTV.Utils;
using RPG.Movement;

namespace RPG.Combat
{

  public class Fighter : Attacker, IStatModifier
  {
    [SerializeField] Transform rightWeaponHolder;
    [SerializeField] Transform leftWeaponHolder;
    [SerializeField] Weapon defaultWeapon;

    LazyValue<Weapon> currentWeapon;

    protected override void Awake()
    {
      base.Awake();
      currentWeapon = new LazyValue<Weapon>(GetInitializeWeapon);
    }

    private Weapon GetInitializeWeapon()
    {
      SpawnWeapon(defaultWeapon);
      return defaultWeapon;
    }

    void Start()
    {
      currentWeapon.ForceInit();
    }

    public override void Attack()
    {
      if (TargetInRange())
      {
        StartCoroutine(StartAttacking());
        mover.Cancel();
      }
      else
      {
        StopAttacking();
        mover.MoveTo(target.transform.position);
      }

      /*damage is dealt by the animation Hit() event*/
      /*projectile for Ranged Weapons is launched within the Shoot() event */
    }

    bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.AttackRange;
    }

    IEnumerator StartAttacking()
    {
      currentlyAttacking = true;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      AdjustAttackDirection();
      yield return new WaitForSeconds(1 / currentWeapon.value.AttackSpeed);
      currentlyAttacking = false;
    }

    GameObject weaponReference = null;
    public void EquipWeapon(Weapon weapon)
    {
      if (weaponReference) Destroy(weaponReference);
      SpawnWeapon(weapon);
      currentWeapon.value = weapon;
    }

    private void SpawnWeapon(Weapon weapon)
    {
      Animator animator = GetComponent<Animator>();
      weaponReference = weapon.Equip(rightWeaponHolder, leftWeaponHolder, animator);
    }

    //animation event (called from animator)
    void Hit()
    {
      if (target == null) return;

      float damage = GetComponent<CharacterStats>().GetStat(Stat.DAMAGE);
      if (currentWeapon.value is RangedWeapon)
      {
        RangedWeapon weapon = (RangedWeapon)currentWeapon.value;
        weapon.LaunchProjectile(rightWeaponHolder, leftWeaponHolder, target, gameObject, collisionLayer, damage);
      }
      else
      {
        if (!TargetInRange()) return;
        target.GetComponent<Health>().ApplyDamage(gameObject, damage);
      }
    }

    //animation event (called from animator)
    void Shoot()
    {
      Hit();
    }

    public IEnumerable GetAdditiveModifiers(Stat stat)
    {
      if (stat == Stat.DAMAGE)
      {
        yield return currentWeapon.value.Damage;
      }
    }

    public IEnumerable GetMultiplicativeModifiers(Stat stat)
    {
      if (stat == Stat.DAMAGE)
      {
        yield return currentWeapon.value.DamageMultiplier;
      }
    }
  }
}