using System;
using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class Armour : ModifiableItem
  {
    [Serializable]
    public class ASaveData : MSaveData
    {
      public float armour, magicResistance;

      public ASaveData(Item item) : base(item)
      {
        Armour armourItem = item as Armour;
        armour = armourItem.armour;
        magicResistance = armourItem.magicResistance;
      }

      public override Item CreateItemFromData()
      {
        Armour mItem = base.CreateItemFromData() as Armour;
        mItem.armour = armour;
        mItem.magicResistance = magicResistance;
        return mItem;
      }
    }

    public float armour, magicResistance;

    public Armour(GenericItem baseItem) : base(baseItem)
    {
      GenericArmour genericArmour = baseItem as GenericArmour;
      armour = genericArmour.GetArmour();
      magicResistance = genericArmour.GetMagicResi();
    }

    public override string GetMainStatText()
    {
      return $"{armour.ToString("F1")} Armour \n{magicResistance.ToString("F1")} Magic Resistance";
    }

    public override void GetStats(ModifyTable stats)
    {
      stats.armourFlat += armour;
      stats.magicResiFlat += magicResistance;
    }

    public override object GetSaveData()
    {
      return new ASaveData(this);
    }
  }
}