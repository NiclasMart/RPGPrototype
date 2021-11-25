using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityGem : Item
  {
    public Ability ability;

    public AbilityGem(GenericItem baseItem) : base(baseItem)
    {
      GenericAbility baseAbility = baseItem as GenericAbility;
      ability = baseAbility.GetAbility();
    }
  }
}