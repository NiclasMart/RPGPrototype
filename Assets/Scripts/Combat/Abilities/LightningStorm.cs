using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class LightningStorm : Ability
  {
    [SerializeField] float duration;
    [SerializeField] TargetDetector areaPrefab;
    TargetDetector detectorInstance;
    CharacterStats stats;

    public override void CastAction()
    {
      if (detectorInstance == null) Initialize();

      float damage = (baseEffectValue + stats.GetStat(Stat.MagicDamageFlat)) * (1 + stats.GetStat(Stat.DamagePercent) / 100f);
      StartCoroutine(CastLightning(damage));
    }

    private void Initialize()
    {
      detectorInstance = Instantiate(areaPrefab);
      detectorInstance.Initialize(data.source.tag);
      stats = data.source.GetComponent<CharacterStats>();
    }

    IEnumerator CastLightning(float damage)
    {
      detectorInstance.transform.position = PlayerInfo.GetPlayerCursor().Position;
      detectorInstance.gameObject.SetActive(true);
      yield return new WaitForSeconds(0.1f);
      foreach (var enemy in detectorInstance.TargetsInArea)
      {
        enemy.ApplyDamage(data.source, damage);
      }

      yield return new WaitForSeconds(duration);
      detectorInstance.gameObject.SetActive(false);
    }

    public override bool CastIsValid(GameObject player)
    {
      return PlayerInfo.GetPlayerCursor().Position != Vector3.zero;
    }
  }
}
