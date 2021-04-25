using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapons/Create New RangedWeapon", order = 0)]
  public class RangedWeapon : Weapon
  {
    [SerializeField] GameObject projectilePrefab;

    Transform projectileSpawnLocation;

    public override Weapon Equip(Transform rightHand, Transform leftHand, Animator animator)
    {
      projectileSpawnLocation = isRightHanded ? rightHand : leftHand;
      return base.Equip(rightHand, leftHand, animator);
    }

    public void LaunchProjectile(Transform target)
    {
      GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnLocation.position, Quaternion.identity);
      if (projectileInstance)
      {
        projectileInstance.GetComponent<Projectile>().Initialize(target, Damage);
      }
    }
  }
}