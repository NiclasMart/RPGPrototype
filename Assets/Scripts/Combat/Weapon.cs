using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  public class Weapon : ModifiableItem
  {
    private AnimationClip animation;
    private float damage;
    private float range;
    private float attackSpeed;
    private DamageClass damageType;
    private protected bool isRightHanded = true;

    public float Damage { get => damage; }
    public float AttackRange { get => range; }
    public float AttackSpeed { get => attackSpeed; }

    public Weapon(GenericItem baseItem) : base(baseItem)
    {
      GenericWeapon baseWeapon = baseItem as GenericWeapon;
      animation = baseWeapon.animation;
      damage = baseWeapon.GetDamage();
      range = baseWeapon.weaponRange;
      attackSpeed = baseWeapon.GetAttackspeed();
      damageType = baseWeapon.damageType;
      isRightHanded = baseWeapon.isRightHanded;
    }

    public virtual GameObject Equip(Transform rightHand, Transform leftHand, Animator animator)
    {
      AnimationHandler.OverrideAnimations(animator, animation, "Attack");
      GameObject spawnedWeapon = Spawn(SelectTransform(rightHand, leftHand));
      return spawnedWeapon;
    }

    public GameObject Spawn(Transform position)
    {
      if (itemObject != null) return MonoBehaviour.Instantiate(itemObject, position);
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
    }

  }
}