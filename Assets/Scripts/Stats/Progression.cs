using System;
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
      public int[] health;
      public int[] damage;
      public int[] experiencePoints;
    }

    [SerializeField] ProgressionCharacterClass[] charakterProgression;

    public int GetHealth(CharakterClass charakterClass, int level)
    {
      ProgressionCharacterClass classProgression = GetClassProgression(charakterClass);
      if (classProgression == null) return 0;
      int health = classProgression.health[level - 1];
      return health;
    }

    public int GetExperiencePoints(CharakterClass charakterClass, int level)
    {
      ProgressionCharacterClass classProgression = GetClassProgression(charakterClass);
      if (classProgression == null) return 0;
      int xpPoints = classProgression.experiencePoints[level - 1];
      return xpPoints;
    }

    private ProgressionCharacterClass GetClassProgression(CharakterClass charakterClass)
    {
      foreach (ProgressionCharacterClass charClass in charakterProgression)
      {
        if (charClass.charakterClass == charakterClass) return charClass;
      }
      return null;
    }
  }
}
