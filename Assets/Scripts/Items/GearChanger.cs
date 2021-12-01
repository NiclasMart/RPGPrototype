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

    public Weapon EquipDefaultWeapon()
    {
      GenericWeapon dWeapon = defaultWeapon ? defaultWeapon : Resources.Load("Prefabs/Unarmed") as GenericWeapon;
      Weapon weapon = dWeapon.GenerateItem() as Weapon;
      return weapon;
    }

    public void EquipGear(Item item)
    {
      if (item as Weapon != null) EquipWeapon(item as Weapon);
    }

    public void UnequipGear(Item item)
    {
      if (item as Weapon != null) EquipDefaultWeapon();
    }

    EquipedWeapon weaponReference = null;
    public void EquipWeapon(Weapon weapon)
    {
      DeleteOldWeapon();
      GetComponent<PlayerFighter>().currentWeapon = InitializeWeapon(weapon);
    }

    private EquipedWeapon InitializeWeapon(Weapon weapon)
    {
      Animator animator = GetComponent<Animator>();
      weaponReference = weapon.Equip(transform, rightWeaponHolder, leftWeaponHolder, animator);
      return weaponReference;
    }

    private void DeleteOldWeapon()
    {
      if (weaponReference == null) return;

      Destroy(weaponReference.hitArea.gameObject);
      Destroy(weaponReference.gameObject);
    }
  }
}