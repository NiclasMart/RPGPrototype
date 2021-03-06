using System;
using UnityEngine;
using RPG.Combat;
using GameDevTV.Utils;

namespace RPG.Items
{
  public class GearChanger : MonoBehaviour
  {
    [Header("Weapon")]
    [SerializeField] Transform rightWeaponHolder;
    [SerializeField] Transform leftWeaponHolder;
    [SerializeField] GenericWeapon defaultWeapon;

    public GenericWeapon GetDefaultWeapon()
    {
      if (defaultWeapon) return defaultWeapon;
      else return Resources.Load("Prefabs/Unarmed") as GenericWeapon;
    }

    public void EquipGear(Item item)
    {
      if (item as Weapon != null) EquipWeapon(item as Weapon);
    }

    EquipedWeapon weaponReference = null;
    public void EquipWeapon(Weapon weapon)
    {
      if (weaponReference) Destroy(weaponReference);
      GetComponent<PlayerFighter>().currentWeapon = InitializeWeapon(weapon);
    }

    private EquipedWeapon InitializeWeapon(Weapon weapon)
    {
      Animator animator = GetComponent<Animator>();
      weaponReference = weapon.Equip(transform, rightWeaponHolder, leftWeaponHolder, animator);
      return weaponReference;
    }
  }
}