namespace RPG.Items
{
  public class ModifyTable
  {
    public float healthFlat, healthPercent;
    public float armourFlat, armourPercent;
    public float magicResiFlat, magicResiPercent;
    public float damageFlat, damagePercent;
    public float staminaFlat, staminaPercent;
    public float movementSpeed;
    public float attackSpeed;
    public float attackRange;

    public float ModifyHealth(float baseStat)
    {
      return (1 + healthPercent / 100) * baseStat + healthFlat;
    }

    public float ModifyArmour(float baseStat)
    {
      return (1 + armourPercent / 100) * baseStat + armourFlat;
    }

    public float ModifyDamage(float baseStat)
    {
      return (1 + damagePercent / 100) * baseStat + damageFlat;
    }

    public float ModifyStamina(float baseStat)
    {
      return (1 + staminaPercent / 100) * baseStat + staminaFlat;
    }

    public float ModifyMovementSpeed(float baseStat)
    {
      return (1 + movementSpeed / 100) * baseStat;
    }

    public float GetAttackSpeed(float baseStat)
    {
      return attackSpeed + baseStat;
    }

    public float GetAttackRange(float baseStat)
    {
      return baseStat + attackRange;
    }
  }
}