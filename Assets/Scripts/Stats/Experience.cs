using System;
using GameDevTV.Utils;
using RPG.Display;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
  public class Experience : MonoBehaviour, IDisplayable, ISaveable
  {
    [SerializeField] float experienceMuliplier = 0.87f;
    [SerializeField] GameObject levelUpParticlePrefab;
    [SerializeField] float soulEnergyMultiplier = 2;
    float currentExperiencePoints;
    LazyValue<float> maxExperiencePoints;

    public ValueChangeEvent valueChange;

    private void Awake()
    {
      maxExperiencePoints = new LazyValue<float>(GetInitializeXP);
    }

    private float GetInitializeXP()
    {
      return GetComponent<CharacterStats>().GetStat(Stat.Experience);
    }

    private void Start()
    {
      maxExperiencePoints.ForceInit();
      valueChange.Invoke(this);
    }

    public void GainExperience(int baseXP, int enemyLevel, float soulEnergy)
    {
      CharacterStats playerStats = GetComponent<CharacterStats>();
      int levelDifferenceToEnemy = playerStats.Level - enemyLevel;
      float gainedExperience = baseXP * Mathf.Pow(experienceMuliplier, levelDifferenceToEnemy);
      gainedExperience *= Mathf.Lerp(1, 2, soulEnergy);

      CalculateNewXPBalance(playerStats, gainedExperience);
      valueChange.Invoke(this);
    }

    private void CalculateNewXPBalance(CharacterStats stats, float gaintXP)
    {
      float levelUpXP = stats.GetStat(Stat.Experience);
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
        maxExperiencePoints.value = stats.GetStat(Stat.Experience);
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

    public object CaptureSaveData(SaveType saveType)
    {
      return currentExperiencePoints;
    }

    public void RestoreSaveData(object data)
    {
      currentExperiencePoints = (float)data;
    }
  }
}