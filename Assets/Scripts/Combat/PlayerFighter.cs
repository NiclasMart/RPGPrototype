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
    
    float lastAttackTime;


    private void Awake()
    {
      inventory = GetComponent<PlayerInventory>();
      stats = GetComponent<CharacterStats>();
    }

    public void Attack(Animator animator, ActionScheduler scheduler, PlayerCursor cursor)
    {
      float timePerAttack = 1 / stats.GetStat(Stat.AttackSpeed);
      if (Time.time < lastAttackTime + timePerAttack) return;

      lastAttackTime = Time.time;
      scheduler.StartAction(this);
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      AdjustAttackDirection(cursor.Position);
    }


    void AdjustAttackDirection(Vector3 lookPoint)
    {
      transform.forward = lookPoint - transform.position;
    }

    public void Cancel()
    {
      Debug.Log("CancelAttack");
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
  }
}