using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Items;
using RPG.Stats;
using UnityEngine;

namespace RPG.Interaction
{
  public class PlayerInventory : Inventory
  {
    [SerializeField] EquipmentSlot defaultSelectedSlot;
    [SerializeField] SortCategory[] sortingTable;

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

    public void AddItem(Item item)
    {
      SimpleInventory inventory = sortDictionary[item.itemType];
      inventory.AddItem(item);
    }

    public void AddItems(List<Item> items)
    {
      foreach (Item item in items)
      {
        AddItem(item);
      }
    }


    public override void SelectSlot(ItemSlot slot)
    {
      EquipmentSlot equipSlot = slot as EquipmentSlot;
      if (!equipSlot) return;

      if (selectedSlot) selectedSlot.Deselect();
      selectedSlot = equipSlot;
    }

    public void RecalculateModifiers()
    {
      ModifyTable table = new ModifyTable();
      foreach (var slot in transform.GetComponentsInChildren<EquipmentSlot>())
      {
        ModifiableItem modItem = slot.item as ModifiableItem;
        if (modItem == null) continue;

        GetModifiersFromItem(table, modItem);
      }
      PlayerInfo.GetPlayer().GetComponent<CharacterStats>().RecalculateStats(table);
    }

    private static void GetModifiersFromItem(ModifyTable table, ModifiableItem modItem)
    {
      modItem.GetStats(table);
      foreach (var modifier in modItem.modifiers)
      {
        modifier.effect.Invoke(table, modifier.value);
      }
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
