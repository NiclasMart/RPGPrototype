using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
  public class PlayerInventory : Inventory
  {
    [SerializeField] SortCategory[] sortingTable;
    [SerializeField] EquipmentSlot defaultSelectedSlot;

    [System.Serializable]
    class SortCategory
    {
      public ItemType itemType;
      public SimpleInventory inventory;
    }
    Dictionary<ItemType, SimpleInventory> sortDictionary = new Dictionary<ItemType, SimpleInventory>();

    private void Awake()
    {
      BuildDictionary();
      InitializeSlots();
    }

    public void AddItems(List<Item> items)
    {
      foreach (Item item in items)
      {
        SimpleInventory inventory = sortDictionary[item.itemType];
        inventory.AddItem(item);
      }
    }

    public override void SelectSlot(ItemSlot slot)
    {
      EquipmentSlot equipSlot = slot as EquipmentSlot;
      if (!equipSlot) return;

      if (selectedSlot) selectedSlot.Deselect();
      selectedSlot = equipSlot;
    }

    private void BuildDictionary()
    {
      foreach (SortCategory category in sortingTable)
      {
        sortDictionary.Add(category.itemType, category.inventory);
      }
    }

    private void InitializeSlots()
    {
      foreach (var slot in transform.GetComponentsInChildren<EquipmentSlot>())
      {
        slot.Initialize(null, this);
        slot.connectedInventory.gameObject.SetActive(false);
      }
      defaultSelectedSlot.connectedInventory.gameObject.SetActive(true);
      selectedSlot = defaultSelectedSlot;
    }
  }
}
