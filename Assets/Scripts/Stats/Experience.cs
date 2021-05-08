using System;
using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class Experience : MonoBehaviour, IDisplayable
  {
    [SerializeField] float experienceMuliplier = 0.87f;
    [SerializeField] GameObject levelUpParticlePrefab;
    float currentExperiencePoints;
    float maxExperiencePoints;

    public ValueChangeEvent valueChange;

    private void Start()
    {
      maxExperiencePoints = GetComponent<CharacterStats>().GetStat(Stat.LEVELUP_EXPERIENCE);
      valueChange.Invoke(this);
    }

    public void GainExperience(int baseXP, int enemyLevel)
    {
      CharacterStats playerStats = GetComponent<CharacterStats>();
      int levelDifferenceToEnemy = playerStats.Level - enemyLevel;
      float gainedExperience = baseXP * Mathf.Pow(experienceMuliplier, levelDifferenceToEnemy);

      CalculateNewXPBalance(playerStats, gainedExperience);
      valueChange.Invoke(this);
    }

    private void CalculateNewXPBalance(CharacterStats stats, float gaintXP)
    {
      float levelUpXP = stats.GetStat(Stat.LEVELUP_EXPERIENCE);
      float newXPBalance = currentExperiencePoints + gaintXP;

      if (newXPBalance >= levelUpXP)
      {
        bool levelUpSuccessful = stats.LevelUp();
        if (!levelUpSuccessful)
        {
          currentExperiencePoints = levelUpXP;
          return;
        }
        Instantiate(levelUpParticlePrefab, transform.position, Quaternion.identity);
        float remainingXP = newXPBalance - levelUpXP;
        currentExperiencePoints = 0;
        CalculateNewXPBalance(stats, remainingXP);
      }
      else
      {
        currentExperiencePoints = newXPBalance;
        maxExperiencePoints = stats.GetStat(Stat.LEVELUP_EXPERIENCE);
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