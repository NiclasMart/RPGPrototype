using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Weapon", order = 0)]
  public class Weapon : ScriptableObject
  {
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] AnimatorOverrideController animationOverride;

    [Header("Weapon Parameters")]
    [SerializeField] float weaponDamage;
    [SerializeField] float weaponRange;
    [SerializeField, Min(0.1f)] float weaponAttackSpeed;
    [SerializeField] DamageClass damageType;
    [SerializeField] protected bool isRightHanded = true;

    public float Damage { get => weaponDamage; }
    public float AttackRange { get => weaponRange; }
    public float AttackSpeed { get => weaponAttackSpeed; }

    public virtual GameObject Equip(Transform rightHand, Transform leftHand, Animator animator)
    {
      Transform handTransform = isRightHanded ? rightHand : leftHand;
      GameObject spawnedWeapon = Spawn(handTransform);

      if (animationOverride != null) animator.runtimeAnimatorController = animationOverride;
      return spawnedWeapon;
    }

    public GameObject Spawn(Transform position)
    {
      if (weaponPrefab != null) return Instantiate(weaponPrefab, position);
      return null;
    }

  }
}
