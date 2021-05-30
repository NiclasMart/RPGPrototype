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
    [SerializeField] float damageMultiplier;
    [SerializeField] protected bool isRightHanded = true;

    public float Damage { get => weaponDamage; }
    public float AttackRange { get => weaponRange; }
    public float AttackSpeed { get => weaponAttackSpeed; }
    public float DamageMultiplier { get => damageMultiplier; }

    public virtual GameObject Equip(Transform rightHand, Transform leftHand, Animator animator)
    {
      OverrideAnimator(animator);
      GameObject spawnedWeapon = Spawn(SelectTransform(rightHand, leftHand));
      return spawnedWeapon;
    }

    private void OverrideAnimator(Animator animator)
    {
      // if (animationOverride != null) animator.runtimeAnimatorController = animationOverride;
      // else
      // {
      //   var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
      //   if (overrideController != null)
      //   {
      //     animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
      //   }
      // }
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
