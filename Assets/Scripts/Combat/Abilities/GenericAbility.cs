using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "GenericAbility", menuName = "Items/Ability/Create New GenericAbility", order = 0)]
  public class GenericAbility : GenericItem
  {
    [SerializeField] Ability abilityPrefab;

    public override Item GenerateItem()
    {
      return new AbilityHolder(this);
    }

    public Ability GetAbility() { return abilityPrefab; }
  }
}