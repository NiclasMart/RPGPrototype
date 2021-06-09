using RPG.Interaction;
using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Weapon", order = 0)]
  public class Weapon : GenericItem
  {
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] AnimationClip animation;

    [Header("Weapon Parameters")]
    [SerializeField] float weaponDamage;
    [SerializeField] float weaponRange;
    [SerializeField, Min(0.1f)] float weaponAttackSpeed;
    [SerializeField] DamageClass damageType;
    [SerializeField] float damageMultiplier;
    [SerializeField] protected bool isRightHanded = true;

    public float Damage { get => weaponDamage; }
    public float AttackRange { get => weaponRange; }
    public float AttackSpeed { get => weaponAttackSpeed; }
    public float DamageMultiplier { get => damageMultiplier; }

    public virtual GameObject Equip(Transform rightHand, Transform leftHand, Animator animator)
    {
      AnimationHandler.OverrideAnimations(animator, animation, "Attack");
      GameObject spawnedWeapon = Spawn(SelectTransform(rightHand, leftHand));
      return spawnedWeapon;
    }

    public GameObject Spawn(Transform position)
    {
      if (weaponPrefab != null) return Instantiate(weaponPrefab, position);
      return null;
    }

    protected Transform SelectTransform(Transform rightHand, Transform leftHand)
    {
      return isRightHanded ? rightHand : leftHand;
    }

  }
}
