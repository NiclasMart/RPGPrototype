using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class Armour : ModifiableItem
  {
    float armour, magicResistance;
    public float ArmourValue { get => armour; }
    public float MagicResiValue { get => magicResistance; }

    public Armour(GenericItem baseItem) : base(baseItem)
    {
      GenericArmour genericArmour = baseItem as GenericArmour;
      armour = genericArmour.GetArmour();
      magicResistance = genericArmour.GetMagicResi();
    }

    public override string GetMainStatText()
    {
      return $"{armour.ToString("F2")} Armour \n{magicResistance.ToString("F2")} Magic Resistance";
    }
  }
}