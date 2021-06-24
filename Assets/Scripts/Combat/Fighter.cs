using UnityEngine;
using RPG.Core;
using RPG.Stats;
using System.Collections;
using RPG.Items;

namespace RPG.Combat
{

  public class Fighter : Attacker
  {
    public GenericWeapon defaultWeapon;
    [SerializeField] Transform rightWeaponHolder, leftWeaponHolder;

    CharacterStats stats;
    EquipedWeapon equipedWeapon;

    protected override void Awake()
    {
      base.Awake();
      stats = GetComponent<CharacterStats>();
      InitializeWeapon();
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

    private void InitializeWeapon()
    {
      if (!defaultWeapon) return;

      Animator animator = GetComponent<Animator>();
      Weapon weapon = defaultWeapon.GenerateItem() as Weapon;
      equipedWeapon = weapon.Equip(rightWeaponHolder, leftWeaponHolder, animator);

      CalculateInitialStats(weapon);
    }

    private void CalculateInitialStats(Weapon weapon)
    {
      ModifyTable statTable = new ModifyTable();
      weapon.GetStats(statTable);
      GetComponent<CharacterStats>().RecalculateStats(statTable);
    }

    bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < stats.GetStat(Stat.AttackRange);
    }

    IEnumerator StartAttacking()
    {
      currentlyAttacking = true;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      AdjustAttackDirection();
      yield return new WaitForSeconds(1 / stats.GetStat(Stat.AttackSpeed));
      currentlyAttacking = false;
    }

    //animation event (called from animator)
    void Hit()
    {
      if (target == null) return;

      float damage = GetComponent<CharacterStats>().GetStat(Stat.Damage);

      equipedWeapon.Attack(target, gameObject, collisionLayer, damage);
    }

    //animation event (called from animator)
    void Shoot()
    {
      Hit();
    }

    void FinishedAttack() { }
  }
}