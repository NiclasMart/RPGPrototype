using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  class DamageCalculator
  {
    public static float CalculatePhysicalDamage(CharacterStats attacker, CharacterStats defender)
    {
      float baseDamage = attacker.GetStat(Stat.Damage);

      //armour calculation
      float armour = defender.GetStat(Stat.Armour);
      float damageReduction = Mathf.Min((5f * Mathf.Pow(armour, 0.5f)) / 100f, 0.9f);
      Debug.Log("Base Damage: " + baseDamage);
      float damage = baseDamage * (1 - damageReduction);
      Debug.Log("Damage after armour: " + damage + " (Reduction: " + damageReduction + ")");

      //level calculation
      float levelDifference = attacker.Level - defender.Level;
      // float levelDamageMultiplier = 0.002f * Mathf.Pow(levelDifference, 3) + 0.09f*levelDifference + 1f;
      float levelDamageMultiplier = 0.075f * levelDifference * levelDifference + 1f;
      damage *= levelDifference < 0 ? 1 / levelDamageMultiplier : levelDamageMultiplier;

      float multiplierForLog = levelDifference < 0 ? 1 / levelDamageMultiplier : levelDamageMultiplier;
      Debug.Log("Damage after level: " + damage + " (Multiplier: " + multiplierForLog + ")");


      return damage;

    }
  }
}