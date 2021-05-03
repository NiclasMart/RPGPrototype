using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
  [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
  public class Progression : ScriptableObject
  {
    [System.Serializable]
    class ProgressionCharacterClass
    {
      public CharakterClass charakterClass;
      public StatList[] stats;
    }

    [System.Serializable]
    class StatList
    {
      public Stat stat;
      public float[] values;
    }

    [SerializeField] ProgressionCharacterClass[] charakterProgression;

    Dictionary<CharakterClass, Dictionary<Stat, float[]>> lookupTable = null;

    public float GetStat(Stat type, CharakterClass charakterClass, int level)
    {
      BuildLookupTable();

      Dictionary<Stat, float[]> statsLookup;
      if (!lookupTable.TryGetValue(charakterClass, out statsLookup))
      {
        Debug.LogError("Can't find Progression for " + charakterClass);
        return 0;
      }

      float[] valueLevels;
      if (!statsLookup.TryGetValue(type, out valueLevels))
      {
        Debug.LogError("Can't find Stat " + type + " in progression for " + charakterClass);
        return 0;
      }

      if (valueLevels.Length < level)
      {
        Debug.LogError("Level " + level + " " + type + " in progression for " + charakterClass + " is not implemented");
        return 0;
      }

      return valueLevels[level - 1];
    }

    private void BuildLookupTable()
    {
      if (lookupTable != null) return;

      lookupTable = new Dictionary<CharakterClass, Dictionary<Stat, float[]>>();

      foreach (ProgressionCharacterClass charClass in charakterProgression)
      {
        Dictionary<Stat, float[]> statTable = new Dictionary<Stat, float[]>();

        foreach (StatList statList in charClass.stats)
        {
          statTable.Add(statList.stat, statList.values);
        }
        lookupTable.Add(charClass.charakterClass, statTable);
      }

    }
  }
}
