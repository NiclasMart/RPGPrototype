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
    public void Initialize(Transform position, Weapon baseItem)
    {
      handPosition = position;
      this.baseItem = baseItem;
    }

    //enemy fighter 
    public virtual void Attack(Health health, GameObject source, LayerMask layer, float damage)
    {
      health.ApplyDamage(source, damage);
    }

    //player fighter
    public List<Health> GetHitTargets(Vector3 position, Vector3 direction, LayerMask layer)
    {
      RaycastHit[] hits = Physics.SphereCastAll(position + direction, 0.5f, direction, 0);
      List<Health> targets = new List<Health>();

      foreach (var hit in hits)
      {
        Health validTarget = hit.transform.GetComponent<Health>();
        if (validTarget && !targets.Contains(validTarget))
        {
          targets.Add(validTarget);
        }
      }
      return targets;
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Transform player = PlayerInfo.GetPlayer().transform;
      Gizmos.DrawWireSphere(player.position + player.forward, 1f);
    }
  }
}