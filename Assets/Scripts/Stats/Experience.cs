using System;
using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class Experience : MonoBehaviour, IDisplayable
  {
    [SerializeField] float experienceMuliplier = 0.87f;
    float currentExperiencePoints;
    float maxExperiencePoints;

    private void Start()
    {
      maxExperiencePoints = GetComponent<BaseStats>().GetLevelupExperience();
      FindObjectOfType<HUDManager>().SetUpExperienceBar(this);
    }

    public void GainExperience(int baseXP, int enemyLevel)
    {
      BaseStats playerStats = GetComponent<BaseStats>();
      int levelDifferenceToEnemy = playerStats.Level - enemyLevel;
      float gainedExperience = baseXP * Mathf.Pow(experienceMuliplier, levelDifferenceToEnemy);

      CalculateNewXPBalance(playerStats, gainedExperience);
    }

    private void CalculateNewXPBalance(BaseStats stats, float gaintXP)
    {
      int levelUpXP = stats.GetLevelupExperience();
      float newXPBalance = currentExperiencePoints + gaintXP;

      if (newXPBalance >= levelUpXP)
      {
        if (!stats.LevelUp())
        {
          currentExperiencePoints = levelUpXP;
          return;
        }
        float remainingXP = newXPBalance - levelUpXP;
        currentExperiencePoints = 0;
        CalculateNewXPBalance(stats, remainingXP);
      }
      else
      {
        currentExperiencePoints = newXPBalance;
        maxExperiencePoints = stats.GetLevelupExperience();
        print("added xp: " + newXPBalance);
      }
    }

    public float GetCurrentValue()
    {
      return currentExperiencePoints;
    }

    public float GetMaxValue()
    {
      return maxExperiencePoints;
    }
  }
}