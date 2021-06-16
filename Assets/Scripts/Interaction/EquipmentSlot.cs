using RPG.Display;
using RPG.Items;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class EquipmentSlot : ItemSlot
  {
    public SimpleInventory connectedInventory;
    PlayerInventory playerInventory;
    GearChanger gearChanger;

    private void Awake()
    {
      GetComponent<Button>().onClick.AddListener(Select);
      gearChanger = FindObjectOfType<GearChanger>();
      playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void Select()
    {
      inventory.SelectSlot(this);
      connectedInventory.onSecondClick += SetGear;
      connectedInventory.gameObject.SetActive(true);
    }

    public override void Deselect()
    {
      connectedInventory.onSecondClick -= SetGear;
      connectedInventory.gameObject.SetActive(false);
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
