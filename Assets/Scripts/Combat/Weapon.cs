using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Weapon", order = 0)]
  public class Weapon : ScriptableObject
  {
    public GameObject weaponPrefab;
    public AnimatorOverrideController animationOverride;

    public void Spawn(Transform handTransform, Animator animator)
    {
      Instantiate(weaponPrefab, handTransform);
      animator.runtimeAnimatorController = animationOverride;
    }
  }
}
