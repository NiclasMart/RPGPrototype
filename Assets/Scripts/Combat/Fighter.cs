using UnityEngine;
using RPG.Movement;
using RPG.Stats;
using System.Collections;
using RPG.Items;
using UnityEngine.AI;

namespace RPG.Combat
{

  public class Fighter : Attacker
  {
    public GenericWeapon defaultWeapon;
    [SerializeField] Transform rightWeaponHolder, leftWeaponHolder;
    EquipedWeapon equipedWeapon;


    protected override void Initialize()
    {
      if (!defaultWeapon)
      {
        defaultWeapon = Resources.Load("Prefabs/Unarmed") as GenericWeapon;
      }

      Animator animator = GetComponent<Animator>();
      Weapon weapon = defaultWeapon.GenerateItem() as Weapon;
      equipedWeapon = weapon.Equip(transform, rightWeaponHolder, leftWeaponHolder, animator);

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
      NavMeshAgent agent = GetComponent<NavMeshAgent>();
      agent.enabled = false;

      if (equipedWeapon.hitArea)
      {
        equipedWeapon.hitArea.Toggle(true);
        equipedWeapon.hitArea.AdjustDirection(transform.forward);
      }

      animator.speed = animationSpeed;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");

      yield return new WaitForSeconds(equipedWeapon.baseItem.animationClip.length * (1 / animationSpeed));
      animator.speed = 1f;
      if (equipedWeapon.hitArea) equipedWeapon.hitArea.Toggle(false);

      if (!GetComponent<Health>().IsDead) agent.enabled = true;

      scheduler.CancelCurrentAction();
      isAttacking = false;
    }

    //animation event (called from animator)
    void Hit()
    {
      if (equipedWeapon.hitArea.TargetsInArea.Count == 0) return;
      if (!target) return;

      float damage = DamageCalculator.CalculatePhysicalDamage(stats, target.GetComponent<CharacterStats>(), equipedWeapon.GetDamage());

      //if deleted change damage in weapon to private
      Debug.Log("Enemy Dealt " + damage + " Damage. (Plain Weapon Damage: " + equipedWeapon.baseItem.damage);

      equipedWeapon.WeaponAction(target, gameObject, collisionLayer, damage);
    }

    //animation event (called from animator)
    void Shoot()
    {
      if (!target) return;

      //float damage = DamageCalculator.CalculatePhysicalDamage(stats, target.GetComponent<CharacterStats>(), equipedWeapon.GetDamage());

      //if deleted change damage in weapon to private
      //Debug.Log("Enemy Dealt " + damage + " Damage. (Plain Weapon Damage: " + equipedWeapon.baseItem.damage);

      equipedWeapon.WeaponAction(target, gameObject, collisionLayer, equipedWeapon.GetDamage());
    }

    void FinishedAttack() { }
  }
}