using System;
using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class Weapon : ModifiableItem
  {
    [Serializable]
    public class WSaveData : MSaveData
    {
      public float damage, attackSpeed;

      public WSaveData(Item item) : base(item)
      {
        Weapon weapon = item as Weapon;
        damage = weapon.damage;
        attackSpeed = weapon.attackSpeed;
      }

      public override Item CreateItemFromData()
      {
        Weapon mItem = base.CreateItemFromData() as Weapon;
        mItem.damage = damage;
        mItem.attackSpeed = attackSpeed;
        return mItem;
      }
    }

    public AnimationClip animationClip;
    public float damage;
    public float staminaUse;
    private float range;
    public float attackSpeed;
    private GameObject hitArea;
    private protected bool isRightHanded = true;

    public Weapon(GenericItem baseItem) : base(baseItem)
    {
      GenericWeapon baseWeapon = baseItem as GenericWeapon;
      animationClip = baseWeapon.animation;
      damage = baseWeapon.GetDamage();
      range = baseWeapon.weaponRange;
      attackSpeed = baseWeapon.GetAttackspeed();
      isRightHanded = baseWeapon.isRightHanded;
      hitArea = baseWeapon.hitArea;
      staminaUse = baseWeapon.weaponStaminaConsumption;
    }

    public EquipedWeapon Equip(Transform carrier, Transform rightHand, Transform leftHand, Animator animator)
    {
      AnimationHandler.OverrideAnimations(animator, animationClip, "Attack");
      EquipedWeapon spawnedWeapon = Spawn(carrier, SelectTransform(rightHand, leftHand));
      return spawnedWeapon;
    }

    public virtual EquipedWeapon Spawn(Transform carrier, Transform position)
    {
      if (itemObject != null)
      {
        GameObject newWeapon = MonoBehaviour.Instantiate(itemObject, position);
        EquipedWeapon equipedWeapon = newWeapon.AddComponent<EquipedWeapon>();
        equipedWeapon.Initialize(carrier, position, hitArea, this);
        return equipedWeapon;
      }
      return null;
    }

    public override string GetMainStatText()
    {
      return $"{damage.ToString("F1")} Attack damage \n{attackSpeed.ToString("F1")} Attack Speed";
    }

    protected Transform SelectTransform(Transform rightHand, Transform leftHand)
    {
      return isRightHanded ? rightHand : leftHand;
    }

    public override void GetStats(ModifyTable stats)
    {
      // stats.physicalDamageFlat += damage;
      stats.attackSpeed += attackSpeed;
      stats.attackRange += range;
    }

    public override object GetSaveData()
    {
      return new WSaveData(this);
    }
  }
}