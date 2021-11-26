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
    }

    public override string GetMainStatText()
    {
      return $"{damage.ToString("F2")} Damage \n{ability.cooldown.ToString("F2")}s Base Cooldown";
    }

    public override object GetSaveData()
    {
      return new ASaveData(this);
    }
  }
}