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
      public float[] health;
      public float[] damage;
    }

    [SerializeField] ProgressionCharacterClass[] charakterProgression;

    public float GetHealth(CharakterClass charakterClass, int level)
    {
      ProgressionCharacterClass classProgression = GetClassProgression(charakterClass);
      if (classProgression == null) return 0;
      float health = classProgression.health[level - 1];
      return health;
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
