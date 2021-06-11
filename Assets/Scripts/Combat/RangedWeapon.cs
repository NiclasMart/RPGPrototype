using RPG.Items;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class RangedWeapon : Weapon
  {
    GameObject projectilePrefab;

    public RangedWeapon(GenericItem baseItem) : base(baseItem) 
    {
      GenericRangedWeapon genericWeapon = baseItem as GenericRangedWeapon;
      projectilePrefab = genericWeapon.projectilePrefab;
    }

    public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject source, LayerMask layer, float damage)
    {
      Transform spawnLocation = SelectTransform(rightHand, leftHand);
      GameObject projectileInstance = MonoBehaviour.Instantiate(projectilePrefab, spawnLocation.position, Quaternion.identity);
      if (projectileInstance)
      {
        projectileInstance.GetComponent<Projectile>().Initialize(target, source, damage, layer);
      }
    }
  }
}