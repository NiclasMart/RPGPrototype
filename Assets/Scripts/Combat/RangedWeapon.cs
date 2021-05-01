using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/Create New RangedWeapon", order = 0)]
  public class RangedWeapon : Weapon
  {
    [SerializeField] GameObject projectilePrefab;

    public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
    {
      Transform spawnLocation = SelectTransform(rightHand, leftHand);
      GameObject projectileInstance = Instantiate(projectilePrefab, spawnLocation.position, Quaternion.identity);
      if (projectileInstance)
      {
        projectileInstance.GetComponent<Projectile>().Initialize(target, Damage, AttackRange);
      }
    }
  }
}