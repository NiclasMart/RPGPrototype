using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "GenericAbility", menuName = "Items/Ability/Create New GenericAbility", order = 0)]
  public class GenericAbility : GenericItem
  {
    [SerializeField] Ability abilityPrefab;
    [SerializeField] Vector2 damage;

    public override Item GenerateItem()
    {
      return new AbilityGem(this);
    }

    public Ability GetAbility() { return abilityPrefab; }

    public float GetDamage() { return GetRandomValueByQuality(damage[0], damage[1]); }
  }
}