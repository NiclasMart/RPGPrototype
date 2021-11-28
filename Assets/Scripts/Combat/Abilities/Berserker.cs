using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class Berserker : Ability
  {
    [SerializeField] float duration;
    [SerializeField] GameObject effect;
    CharacterStats stats;
    GameObject effectInstance;

    public override void CastAction()
    {
      if (stats == null) stats = data.source.GetComponent<CharacterStats>();
      if (effectInstance == null) effectInstance = Instantiate(effect, transform);

      StartCoroutine(ActiveAura());
    }

    IEnumerator ActiveAura()
    {
      float tmpDamageSave;
      effectInstance.SetActive(true);
      tmpDamageSave = stats.GetStat(Stat.DamagePercent);

      stats.statsDisplay.DisplayStat(Stat.DamagePercent, tmpDamageSave + baseEffectValue);
      stats.ChangeStat(Stat.DamagePercent, tmpDamageSave + baseEffectValue);

      yield return new WaitForSeconds(duration);

      if ((int)stats.GetStat(Stat.DamagePercent) == (int)(tmpDamageSave + baseEffectValue))
      {
        stats.ChangeStat(Stat.DamagePercent, tmpDamageSave);
        stats.statsDisplay.DisplayStat(Stat.DamagePercent, tmpDamageSave);
      }
      effectInstance.SetActive(false);
    }
  }
}
