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

    public int Level => level;

    public ValueChangeEvent valueChange;

    private void Start()
    {
      valueChange.Invoke(this);
    }

    public float GetStat(Stat stat)
    {
      float baseStat = progressionSet.GetStat(stat, charakterClass, level);
      float modifiedStat = CalculateModifiers(stat, baseStat);
      return modifiedStat;
    }

    public bool LevelUp()
    {
      if (level == 100) return false;
      level++;
      GetComponent<Health>().LevelUpHealth(this);
      valueChange.Invoke(this);
      return true;
    }

    static int counter = 0;
    private float CalculateModifiers(Stat stat, float modifiedStat)
    {
      foreach (IStatModifier provider in GetComponents<IStatModifier>())
      {
        counter++;
        foreach (float modifier in provider.GetAdditiveModifier(stat))
        {
          modifiedStat += modifier;
        }
      }
      print("Recalculate: " + counter);
      return modifiedStat;
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
