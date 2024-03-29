using System;
using RPG.Items;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class RangedWeapon : Weapon
  {
    [Serializable]
    public class RWSaveData : MSaveData
    {
      public RWSaveData(Item item) : base(item){ }

      public override Item CreateItemFromData()
      {
        RangedWeapon mItem = base.CreateItemFromData() as RangedWeapon;
        return mItem;
      }
    }
    GameObject projectilePrefab;

    public RangedWeapon(GenericItem baseItem) : base(baseItem)
    {
      GenericRangedWeapon genericWeapon = baseItem as GenericRangedWeapon;
      projectilePrefab = genericWeapon.projectilePrefab;
    }

    public override EquipedWeapon Spawn(Transform carriere, Transform position)
    {
      if (itemObject != null)
      {
        GameObject newWeapon = MonoBehaviour.Instantiate(itemObject, position);
        EquipedRangedWeapon equipedWeapon = newWeapon.AddComponent<EquipedRangedWeapon>();
        equipedWeapon.Initialize(position, this, projectilePrefab);
        return equipedWeapon;
      }
      return null;
    }
  }
}