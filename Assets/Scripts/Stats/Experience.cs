using System;
using UnityEngine;

namespace RPG.Stats
{
  public class Experience : MonoBehaviour
  {
    [SerializeField] float experienceMuliplier = 0.87f;
    public float currentExperiencePoints;

    public void AddExperience(int baseXP, int enemyLevel)
    {
      BaseStats playerStats = GetComponent<BaseStats>();
      int levelDifferenceToEnemy = playerStats.Level - enemyLevel;
      float gainedExperience = baseXP * Mathf.Pow(experienceMuliplier, levelDifferenceToEnemy);

      CalculateNewXPBalance(playerStats, gainedExperience);
    }

    private void CalculateNewXPBalance(BaseStats stats, float gaintXP)
    {
      int levelUpXP = stats.GetExperiencePoints();
      float newXPBalance = currentExperiencePoints + gaintXP;

      if (newXPBalance >= levelUpXP)
      {
        if (!stats.LevelUp())
        {
          currentExperiencePoints = levelUpXP;
          return;
        }
        currentExperiencePoints = 0;
        float remainingXP = newXPBalance - levelUpXP;
        CalculateNewXPBalance(stats, remainingXP);
      }
      else
      {
        currentExperiencePoints = newXPBalance;
        print("added xp: " + newXPBalance);
      }
    }
  }
}