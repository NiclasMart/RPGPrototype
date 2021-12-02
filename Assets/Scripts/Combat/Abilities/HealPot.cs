using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class HealPot : Ability
  {
    [SerializeField] float healValue;
    Health health;


    public override void CastAction()
    {
      if (remainingUses == 0) return;

      Debug.Log("Heal " + remainingUses);
      if (health == null) health = data.source.GetComponent<Health>();
      health.HealPercentageMax(healValue);
      remainingUses--;
    }
  }

}