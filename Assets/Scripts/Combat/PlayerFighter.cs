using System;
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
    Stamina stamina;
    AttackColdownDisplay cooldownDisplay;

    float lastAttackTime = Mathf.NegativeInfinity;
    public AlterValue<float> onDealDamage;
    public AlterValue<bool> onBeforeAttack;
    public Action onKill;


    private void Awake()
    {
      inventory = GetComponent<PlayerInventory>();
      stats = GetComponent<CharacterStats>();
      animator = GetComponent<Animator>();
      cursor = GetComponent<PlayerCursor>();
      stamina = GetComponent<Stamina>();
      cooldownDisplay = FindObjectOfType<AttackColdownDisplay>();
    }

    private void Update()
    {
      if (currentWeapon) currentWeapon.hitArea.AdjustDirection(cursor.Position - transform.position);
      cooldownDisplay.UpdateCooldown(lastAttackTime, 1f / stats.GetStat(Stat.AttackSpeed));
      // currentWeapon.hitArea.Toggle(Input.GetKey(KeyCode.Space));
    }

    public void Attack(ActionScheduler scheduler, PlayerCursor cursor)
    {
      float timePerAttack = 1 / stats.GetStat(Stat.AttackSpeed);
      if (Time.time < lastAttackTime + timePerAttack) return;
      if (!stamina.UseStamina(currentWeapon.baseItem.staminaUse)) return;

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

      foreach (var target in targets)
      {
        Debug.Log("----------------");
        bool isCrit = false;
        onBeforeAttack?.Invoke(ref isCrit);
        float damage = DamageCalculator.CalculatePhysicalDamage(stats, target.GetComponent<CharacterStats>(), ref isCrit);
        onDealDamage?.Invoke(ref damage);
        isCrit = false;

        //if deleted change damage in weapon to private
        Debug.Log("Player Dealt " + damage + " Damage. (Plain Weapon Damage: " + currentWeapon.baseItem.damage);
        Debug.Log("----------------");

        if (target.ApplyDamage(gameObject, damage)) ;
      }
    }

    void FinishedAttack()
    {
      currentWeapon.DamageAreaLockState(false);
    }
  }
}