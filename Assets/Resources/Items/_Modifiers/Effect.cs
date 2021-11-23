using System;
using System.Collections;
using RPG.Combat;
using RPG.Core;
using RPG.Items;
using RPG.Stats;
using UnityEngine;


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

  public delegate float AlterStat(float stat);

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
  public static void SoulGain()
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

  public static float RollStaminaReduction(float stamina)
  {
    float newValue = stamina - stamina * (effectValueL2 / 100f);
    return newValue;
  }

  IEnumerator SetMovementSpeed(float time, float value)
  {
    yield return null;
  }

  //--- L2 End ---

}
