using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using RPG.Interaction;
using RPG.Items;
using RPG.Saving;
using UltEvents;
using UnityEngine;

namespace RPG.Stats
{
  public class CharacterStats : MonoBehaviour, IDisplayable, ISaveable
  {
    [SerializeField, Range(1, 100)] int level = 1;
    [SerializeField] CharakterClass charakterClass;
    [SerializeField] Progression progressionSet;
    [SerializeField] PlayerInventory playerInventory;
    public StatsDisplay statsDisplay;

    public int Level => level;
    public ValueChangeEvent valueChange;
    [HideInInspector] public UltEvent<CharacterStats> statsChange;

    Dictionary<Stat, float> activeStats;

    private void Start()
    {
      valueChange.Invoke(this);
    }

    public float GetStat(Stat stat)
    {
      return activeStats[stat];
    }

    public void ChangeStat(Stat stat, float value)
    {
      activeStats[stat] = value;
    }

    public void SetLevel(int level)
    {
      this.level = level;
    }


    public void RecalculateStats(ModifyTable modifierTable)
    {
      activeStats = new Dictionary<Stat, float>();

      CalculateStat(Stat.Health, modifierTable.ModifyHealth);
      CalculateStat(Stat.DamageFlat, modifierTable.ModifyPhysicalDamageFlat);
      CalculateStat(Stat.DamagePercent, modifierTable.ModifyPhysicalDamagePercent);
      CalculateStat(Stat.Armour, modifierTable.ModifyArmour);
      CalculateStat(Stat.MovementSpeed, modifierTable.ModifyMovementSpeed);
      CalculateStat(Stat.Stamina, modifierTable.ModifyStamina);
      CalculateStat(Stat.AttackSpeed, modifierTable.GetAttackSpeed);
      CalculateStat(Stat.AttackRange, modifierTable.GetAttackRange);
      CalculateStat(Stat.CritChance, modifierTable.ModifyCritChance);
      CalculateStat(Stat.CritDamage, modifierTable.ModifyCritDamage);
      CalculateStat(Stat.StaminaRegeneration, modifierTable.ModifyStaminaRegeneration);
      CalculateStat(Stat.CooldownReduction, modifierTable.ModifyCooldownReduction);
      CalculateStat(Stat.MagicDamageFlat, modifierTable.ModifyMagicDamageFlat);
      CalculateStat(Stat.MagicDamagePercent, modifierTable.ModifyMagicDamagePercent);
      CalculateStat(Stat.MagicResi, modifierTable.ModifyMagicResi);

      activeStats.Add(Stat.Experience, progressionSet.GetStat(Stat.Experience, charakterClass, level));
      statsChange.Invoke(this);
    }

    private void CalculateStat(Stat stat, Func<float, float> ModifyStat)
    {
      float baseStat = progressionSet.GetStat(stat, charakterClass, level);
      float modifiedStat = ModifyStat(baseStat);
      activeStats.Add(stat, modifiedStat);
      if (statsDisplay) statsDisplay.DisplayStat(stat, modifiedStat);
    }

    public bool LevelUp()
    {
      if (level == 50) return false;
      level++;
      GetComponent<Health>().LevelUpHealth(this);
      playerInventory.RecalculateModifiers();
      valueChange.Invoke(this);
      return true;
    }

    //gets level
    public float GetCurrentValue()
    {
      return level;
    }

    public float GetMaxValue()
    {
      return 100;
    }

    public object CaptureSaveData(SaveType saveType)
    {
      return level;
    }

    public void RestoreSaveData(object data)
    {
      level = (int)data;
      valueChange.Invoke(this);
      FindObjectOfType<PlayerInventory>().RecalculateModifiers();
    }
  }
}
