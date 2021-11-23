using System;
using System.Collections;
using RPG.Combat;
using RPG.Core;
using RPG.Items;
using RPG.Movement;
using RPG.Stats;
using UnityEngine;

public delegate void AlterValue(ref float stat);

[Serializable]
public class Effect
{
  public static void AddFlatDamage(ModifyTable modifyTable, float value)
  {
    modifyTable.damageFlat += value;
  }

  public static void AddPercentDamage(ModifyTable modifyTable, float value)
  {
    modifyTable.damagePercent += value;
  }

  public static void AddFlatHealth(ModifyTable modifyTable, float value)
  {
    modifyTable.healthFlat += value;
  }

  public static void AddPercentHealth(ModifyTable modifyTable, float value)
  {
    modifyTable.healthPercent += value;
  }

  public static void AddFlatArmour(ModifyTable modifyTable, float value)
  {
    modifyTable.armourFlat += value;
  }

  public static void AddPercentArmour(ModifyTable modifyTable, float value)
  {
    modifyTable.armourPercent += value;
  }

  public static void AddStaminaFlat(ModifyTable modifyTable, float value)
  {
    modifyTable.staminaFlat += value;
  }

  public static void AddPercentStamina(ModifyTable modifyTable, float value)
  {
    modifyTable.staminaPercent += value;
  }

  public static void AddMovementSpeed(ModifyTable modifyTable, float value)
  {
    modifyTable.movementSpeed += value;
  }

  public static void AddCriticalDamage(ModifyTable modifyTable, float value)
  {
    modifyTable.critDamageFlat += value;
  }

  public static void AddCriticalChance(ModifyTable modifyTable, float value)
  {
    modifyTable.critChanceFlat += value;
  }
  public static void AddStaminaRegeneration(ModifyTable modifyTable, float value)
  {
    modifyTable.staminaPerSecond += value;
  }

  //----- Legendary Effects -----

  public class Timer : MonoBehaviour { }
  private static Timer timerInstance;
  private static void InitTimer()
  {
    if (timerInstance == null)
    {
      GameObject go = new GameObject("Timer");
      timerInstance = go.AddComponent<Timer>();
    }
  }



  //--- L1 Start ---
  public static void LegendarySoulGainer_Install(float value)
  {
    SoulEnergy component = PlayerInfo.GetPlayer().GetComponent<SoulEnergy>();
    component.onGetEnergy -= SoulGain;
    component.onGetEnergy += SoulGain;

    if (effectValueL1 == 0) effectValueL1 = (int)value;
    else effectValueL1 = Mathf.Min(effectValueL1, (int)value);
  }

  public static void LegendarySoulGainer_Uninstall()
  {
    SoulEnergy component = PlayerInfo.GetPlayer().GetComponent<SoulEnergy>();
    component.onGetEnergy -= SoulGain;
    effectValueL1 = 0;
  }

  static int soulsGained = 0, effectValueL1;
  private static void SoulGain()
  {
    soulsGained++;

    if (soulsGained == effectValueL1)
    {
      PlayerInfo.GetPlayer().GetComponent<SoulEnergy>().AddEnergy();
      soulsGained = 0;
    }
  }
  //--- L1 End ---

  //--- L2 Start
  public static void LegendaryRollStaminaReduction_Install(float value)
  {
    MoveSkill rollAbility = PlayerInfo.GetPlayer().GetComponent<AbilityManager>().GetRollAbility() as MoveSkill;
    rollAbility.alterStamina -= RollStaminaReduction;
    rollAbility.alterStamina += RollStaminaReduction;

    if (effectValueL2 == 0) effectValueL2 = (int)value;
    else effectValueL2 = Mathf.Max(effectValueL2, (int)value);
  }

  public static void LegendaryRollStaminaReduction_Uninstall()
  {
    MoveSkill rollAbility = PlayerInfo.GetPlayer().GetComponent<AbilityManager>().GetRollAbility() as MoveSkill;
    rollAbility.alterStamina -= RollStaminaReduction;
    effectValueL2 = 0;
  }

  static int effectValueL2;

  private static void RollStaminaReduction(ref float stamina)
  {
    stamina -= (stamina * (effectValueL2 / 100f));
  }

  //--- L2 End ---

  //--- L3 Start---

  public static void LegendaryExperienceGain_Install(float value)
  {
    Experience component = PlayerInfo.GetPlayer().GetComponent<Experience>();
    component.alterExperienceMultiplier -= ExperienceGain;
    component.alterExperienceMultiplier += ExperienceGain;

    if (effectValueL3 == 0) effectValueL3 = (int)value;
    else effectValueL3 = Mathf.Max(effectValueL3, (int)value);
  }

  public static void LegendaryExperienceGain_Uninstall()
  {
    Experience component = PlayerInfo.GetPlayer().GetComponent<Experience>();
    component.alterExperienceMultiplier -= ExperienceGain;
    effectValueL3 = 0;
  }

  static float effectValueL3;

  private static void ExperienceGain(ref float value)
  {
    value += (effectValueL3 / 100f);
  }
  //--- L3 End ---

  // --- L4 Start

  public static void LegendaryRollingSpeed_Install(float value)
  {
    MoveSkill rollAbility = PlayerInfo.GetPlayer().GetComponent<AbilityManager>().GetRollAbility() as MoveSkill;
    rollAbility.onEndCast -= RollingSpeed;
    rollAbility.onEndCast += RollingSpeed;

    if (effectValueL4 == 0) effectValueL4 = value;
    else effectValueL4 = Mathf.Max(effectValueL4, value);
  }

  public static void LegendaryRollingSpeed_Uninstall()
  {
    MoveSkill rollAbility = PlayerInfo.GetPlayer().GetComponent<AbilityManager>().GetRollAbility() as MoveSkill;
    rollAbility.onEndCast -= RollingSpeed;
    effectValueL4 = 0;
  }

  static float effectValueL4;
  static float resetTime;
  static bool effectActiveL4 = false;

  private static void RollingSpeed()
  {
    if (effectActiveL4)
    {
      resetTime = Time.time + effectValueL4;
      return;
    }

    effectActiveL4 = true;
    Mover mover = PlayerInfo.GetPlayer().GetComponent<Mover>();
    float speedChange = mover.MovementSpeed * 0.2f;
    mover.SetMovementSpeed(mover.MovementSpeed + speedChange);

    InitTimer();
    timerInstance.StartCoroutine(SetMovementSpeed(mover, speedChange));
  }

  static IEnumerator SetMovementSpeed(Mover mover, float value)
  {
    resetTime = Time.time + effectValueL4;

    while (Time.time < resetTime)
    {
      yield return new WaitForSeconds(0.5f);
    }

    mover.SetMovementSpeed(mover.MovementSpeed - value);
    effectActiveL4 = false;
  }
  //--- L4 End ---

  //--- L5 Start ---

  public static void LegendaryLifeSaver_Install(float value)
  {
    Health component = PlayerInfo.GetPlayer().GetComponent<Health>();
    component.onTakeDamage -= ActivateLifeSaver;
    component.onTakeDamage += ActivateLifeSaver;

    if (effectValueL5 == 0) effectValueL5 = value;
    else effectValueL5 = Mathf.Max(effectValueL5, value);
  }

  public static void LegendaryLifeSaver_Uninstall()
  {
    Health component = PlayerInfo.GetPlayer().GetComponent<Health>();
    component.onTakeDamage -= ActivateLifeSaver;
    effectValueL5 = 0;
  }

  static float effectValueL5;
  static bool effectActiveL5, cooldownReadyL5 = true;

  private static void ActivateLifeSaver(ref float damage)
  {
    if (effectActiveL5)
    {
      damage -= (damage * (effectValueL5 / 100f));
      return;
    }

    Health health = PlayerInfo.GetPlayer().GetComponent<Health>();
    if (!cooldownReadyL5 || health.CurrentHealth > health.MaxHealth * 0.2f) return;

    effectActiveL5 = true;
    cooldownReadyL5 = false;

    InitTimer();
    timerInstance.StartCoroutine(WaitTimerL5(50, () => {cooldownReadyL5 = true;}));
    timerInstance.StartCoroutine(WaitTimerL5(5, () => {effectActiveL5 = false;}));
  }

  static IEnumerator WaitTimerL5(float time, Action methode)
  {
    float endTime = Time.time + time;

    while (Time.time < endTime)
    {
      yield return new WaitForSeconds(0.5f);
    }

    methode.Invoke();
  }

  //--- L5 End ---





}
