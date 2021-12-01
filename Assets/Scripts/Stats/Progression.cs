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
      public StatData[] stats;
    }

    [System.Serializable]
    class StatData
    {
      public Stat stat;
      public float startValue;
      public float increasement;
    }

    [SerializeField] ProgressionCharacterClass[] charakterProgression;

    Dictionary<CharakterClass, Dictionary<Stat, StatData>> lookupTable = null;

    public float GetStat(Stat stat, CharakterClass charakterClass, int level)
    {
      BuildLookupTable();

      Dictionary<Stat, StatData> characterTable = lookupTable[charakterClass];
      StatData valueLevels;
      if (!characterTable.TryGetValue(stat, out valueLevels)) return 0;

      return valueLevels.startValue + ((level - 1) * valueLevels.increasement);
    }

    private void BuildLookupTable()
    {
      if (lookupTable != null) return;

      lookupTable = new Dictionary<CharakterClass, Dictionary<Stat, StatData>>();

      foreach (ProgressionCharacterClass charClass in charakterProgression)
      {
        Dictionary<Stat, StatData> statTable = new Dictionary<Stat, StatData>();

        foreach (StatData statList in charClass.stats)
        {
          statTable.Add(statList.stat, statList);
        }
        lookupTable.Add(charClass.charakterClass, statTable);
      }

    }
  }
}
