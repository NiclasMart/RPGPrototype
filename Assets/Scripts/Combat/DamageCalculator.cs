using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  class DamageCalculator
  {
    public static float CalculatePhysicalDamage(CharacterStats attacker, CharacterStats defender, float weaponDamage, float damageModifier)
    {
      float baseDamage = attacker.GetStat(Stat.DamageFlat) * (1 + attacker.GetStat(Stat.DamagePercent) / 100f) * damageModifier;
      baseDamage += (weaponDamage * (1 + attacker.GetStat(Stat.DamagePercent) / 100f));

      //armour calculation
      float armour = defender.GetStat(Stat.Armour);
      float damageReduction = Mathf.Min(armour * 0.003f, 0.8f);
      Debug.Log("Base Damage: " + baseDamage);
      float damage = baseDamage * (1 - damageReduction);
      Debug.Log("Damage after armour: " + damage + " (Reduction: " + damageReduction + ")");

      //damage *= LevelMultiplier(attacker.Level - defender.Level);
      Debug.Log("Damage after level: " + damage);

      return damage;
    }

    public static float CalculatePhysicalDamage(CharacterStats attacker, CharacterStats defender, float weaponDamage, float damageModifier, ref bool isCrit)
    {
      float damage = CalculatePhysicalDamage(attacker, defender, weaponDamage, damageModifier);

      //crit calculation
      if (!isCrit) isCrit = (attacker.GetStat(Stat.CritChance) / 100f) > Random.value;
      if (isCrit) damage *= (attacker.GetStat(Stat.CritDamage) / 100);

      Debug.Log("Critical Strike: " + isCrit + " (new damage: " + damage + ")");

      return damage;
    }

    public static float CalculateMagicDamage(CharacterStats attacker, CharacterStats defender, float weaponDamage, float damageModifier)
    {
      float baseDamage = attacker.GetStat(Stat.MagicDamageFlat) * (1 + attacker.GetStat(Stat.MagicDamagePercent) / 100f) * damageModifier;
      baseDamage += (weaponDamage * (1 + attacker.GetStat(Stat.MagicDamagePercent) / 100f));

      //armour calculation
      float magicResi = defender.GetStat(Stat.MagicResi);
      float damageReduction = Mathf.Min(magicResi * 0.003f, 0.8f);
      Debug.Log("Base Magic Damage: " + baseDamage);
      float damage = baseDamage * (1 - damageReduction);
      Debug.Log("Damage after magic Resi: " + damage + " (Reduction: " + damageReduction + ")");

      //damage *= LevelMultiplier(attacker.Level - defender.Level);

      return damage;
    }

    private static float LevelMultiplier(float levelDifference)
    {
      // float levelDamageMultiplier = 0.002f * Mathf.Pow(levelDifference, 3) + 0.09f*levelDifference + 1f;
      float levelDamageMultiplier = 0.05f * levelDifference * levelDifference + 1f;

      float multiplierForLog = levelDifference < 0 ? 1 / levelDamageMultiplier : levelDamageMultiplier - ((levelDifference - 1) * 0.1f);
      Debug.Log("Damage Multiplier after level: " + multiplierForLog + ")");

      return multiplierForLog;
    }


  }
}