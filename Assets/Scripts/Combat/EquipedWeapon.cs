using System.Collections.Generic;
using RPG.Core;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class EquipedWeapon : MonoBehaviour
  {
    public Weapon baseItem;
    protected Transform handPosition;
    public TargetDetector hitArea;

    public void Initialize(Transform carrier, Transform position, GameObject hitArea, Weapon baseItem)
    {
      handPosition = position;
      this.baseItem = baseItem;
      SetHitArea(carrier, hitArea);
    }

    private void SetHitArea(Transform carrier, GameObject hitArea)
    {
      GameObject area = Instantiate(hitArea, carrier.position, hitArea.transform.rotation, carrier);
      this.hitArea = area.GetComponentInChildren<TargetDetector>();
      this.hitArea.Initialize(carrier.tag);
    }

    //enemy fighter 
    public virtual void WeaponAction(Health health, GameObject source, LayerMask layer, float damage)
    {
      health.ApplyDamage(source, damage);
    }

    //player fighter
    public List<Health> GetHitTargets(Vector3 position, Vector3 direction, LayerMask layer)
    {
      return hitArea.TargetsInArea;
    }

    public void DamageAreaLockState(bool locked)
    {
      hitArea.locked = locked;
    }
  }
}