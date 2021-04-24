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

    public float Damage { get => weaponDamage; }
    public float AttackRange { get => weaponRange; }
    public float AttackSpeed { get => weaponAttackSpeed; }

    public void Spawn(Transform handTransform, Animator animator)
    {
      Instantiate(weaponPrefab, handTransform);
      animator.runtimeAnimatorController = animationOverride;
    }


  }
}
