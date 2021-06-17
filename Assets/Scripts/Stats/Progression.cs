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

    public float GetStat(Stat stat, CharakterClass charakterClass, int level)
    {
      BuildLookupTable();

      Dictionary<Stat, float[]> characterTable = lookupTable[charakterClass];
      float[] valueLevels;
      if (!characterTable.TryGetValue(stat, out valueLevels)) return 0;

      if (valueLevels.Length < level)
      {
        Debug.LogError("Level " + level + " " + stat + " in progression for " + charakterClass + " is not implemented");
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
