using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
  public class BaseStats : MonoBehaviour
  {
    [SerializeField, Range(1, 99)] int level = 1;
    [SerializeField] CharakterClass charakterClass;
    [SerializeField] Progression progressionSet;

    public int Level => level;

    public int GetHealth()
    {
      return progressionSet.GetHealth(charakterClass, level);
    }

    public int GetExperiencePoints(){
      return progressionSet.GetExperiencePoints(charakterClass, level);
    }

    public bool LevelUp(){
      if (level == 100) return false;
      level++;
      return true;
    }
  }
}
