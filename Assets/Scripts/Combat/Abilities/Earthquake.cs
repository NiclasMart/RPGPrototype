using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class Earthquake : Ability
  {
    [SerializeField] float duration;
    [SerializeField] TargetDetector areaPrefab;
    TargetDetector detectorInstance;
    CharacterStats stats;

    public override void CastAction()
    {
      if (detectorInstance == null) Initialize();

      float damagePerSecond = baseEffectValue * (stats.GetStat(Stat.DamageFlat) * (1 + stats.GetStat(Stat.DamagePercent) / 100f));
      StartCoroutine(StartEarthquake(damagePerSecond / 2));
    }

    private void Initialize()
    {
      detectorInstance = Instantiate(areaPrefab, transform);
      detectorInstance.Initialize(data.source.tag);
      stats = data.source.GetComponent<CharacterStats>();
    }

    IEnumerator StartEarthquake(float damage)
    {
      detectorInstance.gameObject.SetActive(true);
      float startTime = Time.time;

      while (Time.time < startTime + duration)
      {
        foreach (var enemy in detectorInstance.TargetsInArea)
        {
          enemy.ApplyDamage(data.source, damage);
        }
        yield return new WaitForSeconds(0.5f);
      }
      detectorInstance.gameObject.SetActive(false);
    }
  }

}