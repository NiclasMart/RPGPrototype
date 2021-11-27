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
      public float baseEffectValue;

      public ASaveData(Item item) : base(item)
      {
        AbilityGem abilityGem = item as AbilityGem;
        baseEffectValue = abilityGem.baseEffectValue;
      }

      public override Item CreateItemFromData()
      {
        AbilityGem mItem = base.CreateItemFromData() as AbilityGem;
        mItem.baseEffectValue = baseEffectValue;
        return mItem;
      }
    }
    public Ability ability;
    public float baseEffectValue;

    public AbilityGem(GenericItem baseItem) : base(baseItem)
    {
      GenericAbility baseAbility = baseItem as GenericAbility;
      ability = baseAbility.GetAbility();
      ability.icon = baseAbility.icon;
      baseEffectValue = baseAbility.GetDamage();
      rarity = Rank.Gem;
    }

    public override string GetMainStatText()
    {
      string display = $"{ability.cooldown.ToString("F1")}s Base Cooldown";
      if (ability.staminaConsumption != 0) display += $"\n{ability.staminaConsumption.ToString()} Stamina Consumption";
      return display;
    }

    public string GetDescription()
    {
      return ability.description.Replace("*", baseEffectValue.ToString("F1"));
    }

    public override object GetSaveData()
    {
      return new ASaveData(this);
    }
  }
}