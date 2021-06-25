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


    EquipedWeapon equipedWeapon;


    protected override void Initialize()
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

    protected override IEnumerator StartAttacking()
    {
      isAttacking = true;
      animator.speed = animationSpeed;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      yield return new WaitForSeconds(equipedWeapon.baseItem.animationClip.length * (1 / animationSpeed));
      animator.speed = 1f;
      isAttacking = false;
      scheduler.CancelCurrentAction();
    }

    //animation event (called from animator)
    void Hit()
    {
      if (target == null) return;

      float damage = GetComponent<CharacterStats>().GetStat(Stat.Damage);

      equipedWeapon.WeaponAction(target, gameObject, collisionLayer, damage);
    }

    //animation event (called from animator)
    void Shoot()
    {
      Hit();
    }

    void FinishedAttack() { }
  }
}