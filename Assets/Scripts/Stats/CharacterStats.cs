using System;
using System.Collections;
using RPG.Display;
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

    private void Start()
    {
      valueChange.Invoke(this);
    }

    public float GetStat(Stat stat)
    {
      float baseStat = progressionSet.GetStat(stat, charakterClass, level);
      float multiplicativeModifiers = 0, additiveModifiers = 0;
      if (useModifiers) GetModifiers(stat, out multiplicativeModifiers, out additiveModifiers);
      return baseStat * (1 + multiplicativeModifiers) + additiveModifiers;
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
