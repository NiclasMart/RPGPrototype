using System;
using GameDevTV.Utils;
using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class Experience : MonoBehaviour, IDisplayable
  {
    [SerializeField] float experienceMuliplier = 0.87f;
    [SerializeField] GameObject levelUpParticlePrefab;
    float currentExperiencePoints;
    LazyValue<float> maxExperiencePoints;

    public ValueChangeEvent valueChange;

    private void Awake()
    {
      maxExperiencePoints = new LazyValue<float>(GetInitializeXP);
    }

    private float GetInitializeXP()
    {
      return GetComponent<CharacterStats>().GetStat(Stat.LEVELUP_EXPERIENCE);
    }

    private void Start()
    {
      maxExperiencePoints.ForceInit();
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
        maxExperiencePoints.value = stats.GetStat(Stat.LEVELUP_EXPERIENCE);
        print("added xp: " + newXPBalance);
      }
    }

    public float GetCurrentValue()
    {
      return currentExperiencePoints;
    }

    public float GetMaxValue()
    {
      return maxExperiencePoints.value;
    }
  }
}