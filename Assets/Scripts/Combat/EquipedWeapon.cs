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

    public void Initialize(Transform position, GameObject hitArea, Weapon baseItem)
    {
      handPosition = position;

      Transform player = PlayerInfo.GetPlayer().transform;
      GameObject area = Instantiate(hitArea, player.position, hitArea.transform.rotation, player);
      this.hitArea = area.GetComponentInChildren<TargetDetector>();
    }

    //enemy fighter 
    public virtual void Attack(Health health, GameObject source, LayerMask layer, float damage)
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