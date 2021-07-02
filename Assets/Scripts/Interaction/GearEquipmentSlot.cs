using RPG.Items;
using UnityEngine;

namespace RPG.Interaction
{
  public class GearEquipmentSlot : EquipmentSlot
  {
    GearChanger gearChanger;

    protected override void Awake()
    {
      base.Awake();
      gearChanger = FindObjectOfType<GearChanger>();
    }

    public override void EquipItem(Item item)
    {
      base.EquipItem(item);
      gearChanger.EquipGear(item);
      playerInventory.RecalculateModifiers();
    }
  }
}