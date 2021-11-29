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
      connectedInventory.onRightClick += EquipItem;
    }

    public override void Deselect()
    {
      border.color = borderDefaultColor;
      connectedInventory.transform.GetChild(0).gameObject.SetActive(false);
      connectedInventory.onRightClick -= EquipItem;
    }

    public virtual void EquipItem(ItemSlot itemSlot)
    {
      EquipItem(itemSlot.item);
    }

    public virtual void EquipItem(Item item)
    {
      if (this.item != null) UnequipCurrentItem();
      SetIcon(item);
      this.item = item;
      connectedInventory.DeleteItem(item);
    }

    protected virtual void UnequipCurrentItem()
    {
      connectedInventory.AddItem(item);
      if (item.rarity == Rank.Legendary) (item as ModifiableItem).GetLegendaryModifier().legendaryUninstallEffect.Invoke();
    }

    public override void HandleRightClick()
    {
      if (item == null) return;

      ToggleItemModifiers(false);
      UnequipCurrentItem();
      SetIcon(null);
      this.item = null;
      if (equipmentType == ItemType.Weapon) playerInventory.TryEquipBaseWeapon();
      else playerInventory.RecalculateModifiers();
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
