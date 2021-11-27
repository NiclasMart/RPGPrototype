using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class BloodAura : Ability
  {
    [SerializeField] float duration;
    Health health;
    public override void CastAction()
    {
      if (health == null) health = data.source.GetComponent<Health>();
      float maxLife = health.GetMaxValue();
      float healPerSec = maxLife * (baseEffectValue / 100f);

      health.ApplyDamage(gameObject, maxLife * 0.2f);
      StartCoroutine(ActiveHeal(healPerSec));
    }

    IEnumerator ActiveHeal(float healPerSec)
    {
      float startTime = Time.time;
      while (Time.time < startTime + duration)
      {
        health.HealAbsolut((int)healPerSec / 2);
        yield return new WaitForSeconds(0.5f);
      }
    }
  }
}

