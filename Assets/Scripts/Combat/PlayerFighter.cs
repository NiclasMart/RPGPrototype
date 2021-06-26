using System.Collections.Generic;
using RPG.Core;
using RPG.Interaction;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class PlayerFighter : MonoBehaviour, IAction
  {
    [HideInInspector] public EquipedWeapon currentWeapon;
    PlayerInventory inventory;
    CharacterStats stats;
    Animator animator;
    PlayerCursor cursor;

    float lastAttackTime;


    private void Awake()
    {
      inventory = GetComponent<PlayerInventory>();
      stats = GetComponent<CharacterStats>();
      animator = GetComponent<Animator>();
      cursor = GetComponent<PlayerCursor>();
    }

    private void Update()
    {
      if (currentWeapon) currentWeapon.hitArea.AdjustDirection(cursor.Position - transform.position);
      currentWeapon.hitArea.Toggle(Input.GetKey(KeyCode.Space));
    }

    public void Attack(ActionScheduler scheduler, PlayerCursor cursor)
    {
      float timePerAttack = 1 / stats.GetStat(Stat.AttackSpeed);
      if (Time.time < lastAttackTime + timePerAttack) return;

      lastAttackTime = Time.time;
      scheduler.StartAction(this);
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");

      currentWeapon.hitArea.AdjustDirection(cursor.Position - transform.position);
      currentWeapon.DamageAreaLockState(true);
    }

    public void Cancel()
    {
      animator.SetTrigger("cancelAttack");
    }

    void Hit()
    {
      List<Health> targets = currentWeapon.GetHitTargets(transform.position, transform.forward, gameObject.layer);
      float damage = stats.GetStat(Stat.Damage);

      foreach (var target in targets)
      {
        target.ApplyDamage(gameObject, damage);
      }
    }

    void FinishedAttack()
    {
      currentWeapon.DamageAreaLockState(false);
    }
  }
}