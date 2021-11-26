using System;
using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityGem : ModifiableItem
  {
    [Serializable]
    public class ASaveData : SaveData
    {
      public float damage;

      public ASaveData(Item item) : base(item)
      {
        AbilityGem abilityGem = item as AbilityGem;
        damage = abilityGem.damage;
      }

      public override Item CreateItemFromData()
      {
        AbilityGem mItem = base.CreateItemFromData() as AbilityGem;
        mItem.damage = damage;
        return mItem;
      }
    }
    public Ability ability;
    public float damage;

    public AbilityGem(GenericItem baseItem) : base(baseItem)
    {
      GenericAbility baseAbility = baseItem as GenericAbility;
      ability = baseAbility.GetAbility();
      damage = baseAbility.GetDamage();
      rarity = Rank.Gem;
    }

    public override string GetMainStatText()
    {
      return $"{ability.cooldown.ToString("F1")}s Base Cooldown";
    }

    public string GetDescription()
    {
      return ability.description.Replace("*", damage.ToString("F1"));
    }

    public override object GetSaveData()
    {
      return new ASaveData(this);
    }
  }
}