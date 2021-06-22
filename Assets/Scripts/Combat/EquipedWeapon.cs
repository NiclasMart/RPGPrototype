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

    public virtual void Attack(Health health, GameObject source, LayerMask layer, float damage)
    {
      health.ApplyDamage(source, damage);
    }
  }
}