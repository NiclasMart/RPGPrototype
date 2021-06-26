using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class Weapon : ModifiableItem
  {
    public AnimationClip animationClip;
    public float damage;
    private float range;
    private float attackSpeed;
    private GameObject hitArea;
    private DamageClass damageType;
    private protected bool isRightHanded = true;

    public Weapon(GenericItem baseItem) : base(baseItem)
    {
      GenericWeapon baseWeapon = baseItem as GenericWeapon;
      animationClip = baseWeapon.animation;
      damage = baseWeapon.GetDamage();
      range = baseWeapon.weaponRange;
      attackSpeed = baseWeapon.GetAttackspeed();
      damageType = baseWeapon.damageType;
      isRightHanded = baseWeapon.isRightHanded;
      hitArea = baseWeapon.hitArea;
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
      return $"{damage.ToString("F2")} Attack damage \n{attackSpeed.ToString("F2")} Attack Speed";
    }

    protected Transform SelectTransform(Transform rightHand, Transform leftHand)
    {
      return isRightHanded ? rightHand : leftHand;
    }

    public override void GetStats(ModifyTable stats)
    {
      stats.damageFlat += damage;
      stats.attackSpeed += attackSpeed;
      stats.attackRange += range;
    }
  }
}