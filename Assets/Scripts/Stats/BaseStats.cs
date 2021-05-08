using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class BaseStats : MonoBehaviour, IDisplayable
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

    public int GetHealth()
    {
      return (int)progressionSet.GetStat(Stat.HEALTH, charakterClass, level);
    }

    public int GetExperienceReward()
    {
      return (int)progressionSet.GetStat(Stat.EXPERIENCE_REWARD, charakterClass, level);
    }

    public int GetLevelupExperience()
    {
      return (int)progressionSet.GetStat(Stat.LEVELUP_EXPERIENCE, charakterClass, level);
    }

    public bool LevelUp()
    {
      if (level == 100) return false;
      level++;
      GetComponent<Health>().LevelUpHealth(this);
      valueChange.Invoke(this);
      return true;
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
