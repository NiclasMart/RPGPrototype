using UnityEngine;
using RPG.Stats;
using RPG.Items;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "GenericRangedWeapon", menuName = "Weapons/Create New GenericRangedWeapon", order = 0)]
  public class GenericRangedWeapon : GenericWeapon
  {
    public GameObject projectilePrefab;


    public override Item GenerateItem()
    {
      return new RangedWeapon(this);
    }
  }
}