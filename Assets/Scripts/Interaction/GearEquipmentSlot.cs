using RPG.Items;
using UnityEngine;

namespace RPG.Interaction
{
  public class GearEquipmentSlot : EquipmentSlot
  {
    GearChanger gearChanger;

    public override void Initialize(Item item, Inventory inventory)
    {
      base.Initialize(item, inventory);
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