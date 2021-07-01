using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityHolder : Item
  {
    public Ability ability;

    public AbilityHolder(GenericItem baseItem) : base(baseItem)
    {
      GenericAbility baseAbility = baseItem as GenericAbility;
      ability = baseAbility.GetAbility();
    }
  }
}