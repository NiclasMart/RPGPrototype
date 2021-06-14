using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Items;
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

    private void Start()
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
        InitializeInventory(category.inventory);
        sortDictionary.Add(category.itemType, category.inventory);
      }
    }

    private static void InitializeInventory(Inventory inventory)
    {
      inventory.InitializeColorParameters();
      inventory.gameObject.SetActive(false);
    }

    private void InitializeSlots()
    {
      foreach (var slot in transform.GetComponentsInChildren<EquipmentSlot>())
      {
        slot.Initialize(null, this);
        slot.connectedInventory.gameObject.SetActive(false);
      }
    }
  }
}
