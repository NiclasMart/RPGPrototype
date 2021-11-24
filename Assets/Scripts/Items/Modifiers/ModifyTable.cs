namespace RPG.Items
{
  [System.Serializable]
  public class ModifyTable
  {
    public float healthFlat, healthPercent;
    public float armourFlat, armourPercent;
    public float magicResiFlat, magicResiPercent;
    public float physicalDamageFlat, physicalDamagePercent;
    public float magicDamageFlat, magicDamagePercent;
    public float staminaFlat, staminaPercent;
    public float critChanceFlat, critDamageFlat;
    public float staminaPerSecond;
    public float movementSpeed;
    public float attackSpeed;
    public float attackRange;
    public float cooldownReduction;

    public float ModifyHealth(float baseStat)
    {
      return (1 + healthPercent / 100) * baseStat + healthFlat;
    }

    public float ModifyArmour(float baseStat)
    {
      return (1 + armourPercent / 100) * baseStat + armourFlat;
    }

    public float ModifyMagicResi(float baseStat)
    {
      return (1 + magicResiPercent / 100) * baseStat + magicResiFlat;
    }

    public float ModifyPhysicalDamage(float baseStat)
    {
      return (1 + physicalDamagePercent / 100) * (baseStat + physicalDamageFlat);
    }

    public float ModifyMagicDamage(float baseStat)
    {
      return (1 + magicDamagePercent / 100) * (baseStat + magicDamageFlat);
    }

    public float ModifyStamina(float baseStat)
    {
      return (1 + staminaPercent / 100) * baseStat + staminaFlat;
    }

    public float ModifyMovementSpeed(float baseStat)
    {
      return (1 + movementSpeed / 100) * baseStat;
    }

    public float ModifyCooldownReduction(float baseStat)
    {
      return cooldownReduction + baseStat;
    }

    public float GetAttackSpeed(float baseStat)
    {
      return attackSpeed + baseStat;
    }

    public float GetAttackRange(float baseStat)
    {
      return baseStat + attackRange;
    }

    public float ModifyCritChance(float baseStat)
    {
      return baseStat + critChanceFlat;
    }

    public float ModifyCritDamage(float baseStat)
    {
      return baseStat + critDamageFlat;
    }

    public float ModifyStaminaRegeneration(float baseStat)
    {
      return baseStat + staminaPerSecond;
    }
  }
}