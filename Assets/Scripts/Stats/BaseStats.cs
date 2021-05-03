using UnityEngine;

namespace RPG.Stats
{
  public class BaseStats : MonoBehaviour
  {
    [SerializeField, Range(1, 100)] int level = 1;
    [SerializeField] CharakterClass charakterClass;
    [SerializeField] Progression progressionSet;

    public int Level => level;

    public int GetHealth()
    {
      return (int)progressionSet.GetStat(Stat.HEALTH, charakterClass, level);
    }

    public int GetExperiencePoints()
    {
      return (int)progressionSet.GetStat(Stat.EXPERIENCE, charakterClass, level);
    }

    public bool LevelUp()
    {
      if (level == 100) return false;
      level++;
      GetComponent<Health>().LevelUpHealth(this);
      return true;
    }
  }
}
