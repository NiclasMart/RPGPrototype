using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  class DamageCalculator
  {
    public static float CalculatePhysicalDamage(CharacterStats attacker, CharacterStats defender, float weaponDamage)
    {
      float baseDamage = attacker.GetStat(Stat.Damage) + weaponDamage;

      //armour calculation
      float armour = defender.GetStat(Stat.Armour);
      float damageReduction = Mathf.Min(0.05f * Mathf.Pow(armour, 0.5f), 0.9f);
      Debug.Log("Base Damage: " + baseDamage);
      float damage = baseDamage * (1 - damageReduction);
      Debug.Log("Damage after armour: " + damage + " (Reduction: " + damageReduction + ")");

      damage *= LevelMultiplier(attacker.Level - defender.Level);

      return damage;
    }

    public static float CalculatePhysicalDamage(CharacterStats attacker, CharacterStats defender, float weaponDamage, ref bool isCrit)
    {
      float damage = CalculatePhysicalDamage(attacker, defender, weaponDamage);

      //crit calculation
      if (!isCrit) isCrit = (attacker.GetStat(Stat.CritChance) / 100f) > Random.value;
      if (isCrit) damage *= (attacker.GetStat(Stat.CritDamage) / 100);

      Debug.Log("Critical Strike: " + isCrit + " (new damage: " + damage + ")");

      return damage;
    }

    public static float CalculateMagicDamage(CharacterStats attacker, CharacterStats defender, float weaponDamage)
    {
      float baseDamage = attacker.GetStat(Stat.MagicDamage) + weaponDamage;

      //armour calculation
      float magicResi = defender.GetStat(Stat.MagicResi);
      float damageReduction = Mathf.Min(0.05f * Mathf.Pow(magicResi, 0.5f), 0.9f);
      Debug.Log("Base Magic Damage: " + baseDamage);
      float damage = baseDamage * (1 - damageReduction);
      Debug.Log("Damage after magic Resi: " + damage + " (Reduction: " + damageReduction + ")");

      damage *= LevelMultiplier(attacker.Level - defender.Level);

      return damage;
    }

    private static float LevelMultiplier(float levelDifference)
    {
      // float levelDamageMultiplier = 0.002f * Mathf.Pow(levelDifference, 3) + 0.09f*levelDifference + 1f;
      float levelDamageMultiplier = 0.075f * levelDifference * levelDifference + 1f;

      // float multiplierForLog = levelDifference < 0 ? 1 / levelDamageMultiplier : levelDamageMultiplier;
      // Debug.Log("Damage after level: " + damage + " (Multiplier: " + multiplierForLog + ")");

      return levelDifference < 0 ? 1 / levelDamageMultiplier : levelDamageMultiplier;
    }


  }
}