using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class MagicArmor : Ability
  {
    [SerializeField] float duration;
    CharacterStats stats;
    public override void CastAction()
    {
      if (stats == null) stats = data.source.GetComponent<CharacterStats>();
      float magicDamage = stats.GetStat(Stat.MagicDamageFlat) * (1 + stats.GetStat(Stat.MagicDamagePercent) / 100f);

      float multipicator = magicDamage * (baseEffectValue / 100f);
      StartCoroutine(ActiveAura(1 + multipicator));
    }

    IEnumerator ActiveAura(float multipicator)
    {
      float tmpArmorSave, tmpMagicResiSave;
      tmpArmorSave = stats.GetStat(Stat.Armour);
      tmpMagicResiSave = stats.GetStat(Stat.MagicResi);

      stats.statsDisplay.DisplayStat(Stat.Armour, tmpArmorSave * multipicator);
      stats.statsDisplay.DisplayStat(Stat.MagicResi, tmpMagicResiSave * multipicator);

      stats.ChangeStat(Stat.Armour, tmpArmorSave * multipicator);
      stats.ChangeStat(Stat.MagicResi, tmpMagicResiSave * multipicator);

      yield return new WaitForSeconds(duration);

      if ((int)stats.GetStat(Stat.Armour) == (int)(tmpArmorSave * multipicator) && (int)stats.GetStat(Stat.MagicResi) == (int)(tmpMagicResiSave * multipicator))
      {
        stats.ChangeStat(Stat.Armour, tmpArmorSave);
        stats.ChangeStat(Stat.MagicResi, tmpMagicResiSave);

        stats.statsDisplay.DisplayStat(Stat.Armour, tmpArmorSave);
        stats.statsDisplay.DisplayStat(Stat.MagicResi, tmpMagicResiSave);
      }
    }
  }
}