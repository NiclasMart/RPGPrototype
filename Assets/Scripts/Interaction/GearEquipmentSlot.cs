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

    public override void Select()
    {
      base.Select();
      connectedInventory.onSecondClick += SetGear;
    }

    public override void Deselect()
    {
      base.Deselect();
      connectedInventory.onSecondClick -= SetGear;
    }

    public void SetGear(Item item)
    {
      SetIcon(item);
      gearChanger.EquipGear(item);
      this.item = item;
      playerInventory.RecalculateModifiers();
    }
  }
}