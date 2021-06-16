using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using RPG.Items;
using UltEvents;
using UnityEngine;

namespace RPG.Stats
{
  public class CharacterStats : MonoBehaviour, IDisplayable
  {
    [SerializeField, Range(1, 100)] int level = 1;
    [SerializeField] CharakterClass charakterClass;
    [SerializeField] Progression progressionSet;
    [SerializeField] bool useModifiers = false;

    public int Level => level;
    public ValueChangeEvent valueChange;
    [HideInInspector] public UltEvent<CharacterStats> statsChange;

    Dictionary<Stat, float> activeStats;

    private void Awake()
    {
      RecalculateStats(new ModifyTable());
    }

    private void Start()
    {
      valueChange.Invoke(this);
    }

    public float GetStat(Stat stat)
    {
      return activeStats[stat];
    }

    public void RecalculateStats(ModifyTable modifierTable)
    {
      activeStats = new Dictionary<Stat, float>();

      //health
      float baseStat = progressionSet.GetStat(Stat.Health, charakterClass, level);
      activeStats.Add(Stat.Health, modifierTable.ModifyHealth(baseStat));
      //armour
      baseStat = progressionSet.GetStat(Stat.Armour, charakterClass, level);
      activeStats.Add(Stat.Armour, modifierTable.ModifyArmour(baseStat));
      //damage
      baseStat = progressionSet.GetStat(Stat.Damage, charakterClass, level);
      activeStats.Add(Stat.Damage, modifierTable.ModifyDamage(baseStat));
      //experience
      baseStat = progressionSet.GetStat(Stat.Experience, charakterClass, level);
      activeStats.Add(Stat.Experience, baseStat);

      statsChange.Invoke(this);
      //more
    }

    public bool LevelUp()
    {
      if (level == 100) return false;
      level++;
      GetComponent<Health>().LevelUpHealth(this);
      valueChange.Invoke(this);
      return true;
    }

    private void GetModifiers(Stat stat, out float multiplicativeModifiers, out float additiveMultipliers)
    {
      float multModifier = 0, addModifier = 0;
      foreach (IStatModifier provider in GetComponents<IStatModifier>())
      {
        foreach (float modifier in provider.GetAdditiveModifiers(stat))
        {
          addModifier += modifier;
        }

        foreach (float modifier in provider.GetMultiplicativeModifiers(stat))
        {
          multModifier += modifier;
        }
      }
      additiveMultipliers = addModifier;
      multiplicativeModifiers = multModifier;
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

  }
}
