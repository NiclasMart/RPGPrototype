using UnityEngine;

namespace RPG.Items
{
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
    
  }
}