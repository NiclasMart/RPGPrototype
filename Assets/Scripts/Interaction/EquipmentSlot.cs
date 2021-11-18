using RPG.Display;
using RPG.Items;
using RPG.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class EquipmentSlot : ItemSlot, ISaveable
  {
    public ItemType equipmentType;
    public SimpleInventory connectedInventory;
    [SerializeField] Image border;
    [SerializeField] Color selectionColor;
    Color borderDefaultColor;
    protected PlayerInventory playerInventory;


    public override void Initialize(Item item, Inventory inventory)
    {
      base.Initialize(item, inventory);
      borderDefaultColor = border.color;
      GetComponent<Button>().onClick.AddListener(Select);
      playerInventory = FindObjectOfType<PlayerInventory>();

    }

    public override void Select()
    {
      inventory.SelectSlot(this);
      border.color = selectionColor;
      connectedInventory.transform.GetChild(0).gameObject.SetActive(true);
      connectedInventory.onSecondClick += EquipItem;
    }

    public override void Deselect()
    {
      border.color = borderDefaultColor;
      connectedInventory.transform.GetChild(0).gameObject.SetActive(false);
      connectedInventory.onSecondClick -= EquipItem;
    }

    public virtual void EquipItem(Item item)
    {
      if (this.item != null) UnequipCurrentItem();
      connectedInventory.DeleteSelectedItem();

      SetIcon(item);
      this.item = item;
    }

    protected void UnequipCurrentItem()
    {
      connectedInventory.AddItem(item);
    }

    public object CaptureSaveData(SaveType saveType)
    {
      return (item != null) ? item.GetSaveData() : null;
    }

    public void RestoreSaveData(object data)
    {
      item = null;
      Item.SaveData itemData = (data as Item.SaveData);
      EquipItem(itemData != null ? itemData.CreateItemFromData() : null);
    }
  }
}
