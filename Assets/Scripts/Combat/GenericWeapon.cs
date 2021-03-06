using RPG.Interaction;
using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "GenericWeapon", menuName = "Items/Weapons/Create New GenericWeapon", order = 0)]
  public class GenericWeapon : GenericItem
  {
    public AnimationClip animation;

    [Header("Weapon Parameters")]
    public Vector2 weaponDamage;
    public float weaponStaminaConsumption;
    [Min(0.1f)] public Vector2 weaponAttackSpeed;
    public float weaponRange;
    public GameObject hitArea;
    public DamageClass damageType;
    public bool isRightHanded = true;

    public override Item GenerateItem()
    {
      return new Weapon(this);
    }

    public float GetDamage() { return Random.Range(weaponDamage.x, weaponDamage.y); }
    public float GetAttackspeed() { return Random.Range(weaponAttackSpeed.x, weaponAttackSpeed.y); }

  }
}
