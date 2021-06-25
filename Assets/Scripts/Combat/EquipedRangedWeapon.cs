using RPG.Movement;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class EquipedRangedWeapon : EquipedWeapon
  {
    GameObject projectilePrefab;

    public void Initialize(Transform position, RangedWeapon baseItem, GameObject projectile)
    {
      handPosition = position;
      this.baseItem = baseItem;
      projectilePrefab = projectile;
    }

    public override void WeaponAction(Health target, GameObject source, LayerMask layer, float damage)
    {
      source.GetComponent<Mover>().AdjustDirection(target.transform.position);
      LaunchProjectile(source, layer, damage);
    }

    private void LaunchProjectile(GameObject source, LayerMask layer, float damage)
    {
      GameObject projectileInstance = MonoBehaviour.Instantiate(projectilePrefab, handPosition.position, Quaternion.identity);
      if (projectileInstance)
      {
        projectileInstance.GetComponent<Projectile>().Initialize(source.transform.forward, source, damage, layer);
      }
    }
  }
}