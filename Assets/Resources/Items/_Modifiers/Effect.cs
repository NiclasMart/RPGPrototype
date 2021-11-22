using System;
using RPG.Core;
using RPG.Items;
using RPG.Stats;
using UnityEngine;


[Serializable]
public static class Effect
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

  //--- L1 Start ---
  public static void LegendarySoulGainer_Install(float value)
  {
    PlayerInfo.GetPlayer().GetComponent<SoulEnergy>().onGetEnergy -= SoulGain;

    PlayerInfo.GetPlayer().GetComponent<SoulEnergy>().onGetEnergy += SoulGain;
    if (value == 0) effectValueL1 = (int)value;
    else effectValueL1 = Mathf.Min(effectValueL1, (int)value);
    Debug.Log("Install with " + value);
  }

  static int soulsGained = 0, effectValueL1;
  public static void SoulGain()
  {
    soulsGained++;
    Debug.Log("Called Legendary Effect " + soulsGained);

    if (soulsGained == effectValueL1)
    {
      PlayerInfo.GetPlayer().GetComponent<SoulEnergy>().AddEnergy();
      soulsGained = 0;
    }
  }
  //--- L1 End ---

}
