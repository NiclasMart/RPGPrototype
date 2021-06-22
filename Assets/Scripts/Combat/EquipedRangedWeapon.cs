using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class EquipedRangedWeapon : EquipedWeapon
  {
    public RangedWeapon baseItem;
    GameObject projectilePrefab;

    public void Initialize(Transform position, RangedWeapon baseItem, GameObject projectile)
    {
      handPosition = position;
      this.baseItem = baseItem;
      projectilePrefab = projectile;
    }

    public override void Attack(Health health, GameObject source, LayerMask layer, float damage)
    {
      LaunchProjectile(health, source, layer, damage);
    }

    private void LaunchProjectile(Health target, GameObject source, LayerMask layer, float damage)
    {
      GameObject projectileInstance = MonoBehaviour.Instantiate(projectilePrefab, handPosition.position, Quaternion.identity);
      if (projectileInstance)
      {
        projectileInstance.GetComponent<Projectile>().Initialize(target, source, damage, layer);
      }
    }
  }
}