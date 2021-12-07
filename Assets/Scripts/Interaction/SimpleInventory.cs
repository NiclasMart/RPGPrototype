using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using RPG.Saving;
using RPG.Items;
using UltEvents;
using RPG.Core;

namespace RPG.Interaction
{
  public class SimpleInventory : Inventory, ISaveable
  {
    [SerializeField] SaveType saveRestriction;

    [Header("UI Specifications")]
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] RectTransform list;

    [HideInInspector] public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public Action<ItemSlot> onRightClick = (itemSlot) => { };
    public Action<Item> onDoubleClick = (item) => { };


    public List<Item> GetItemList()
    {
      List<Item> itemList = new List<Item>();
      foreach (ItemSlot slot in itemSlots)
      {
        itemList.Add(slot.item);
      }
      return itemList;
    }

    public List<Item> GetItemsByRank(Rank rank)
    {
      List<Item> itemList = new List<Item>();
      foreach (ItemSlot slot in itemSlots)
      {
        if (slot.item.rarity == rank) itemList.Add(slot.item);
      }
      return itemList;
    }

    public void AddItem(Item item)
    {
      Debug.Log(item.itemID + " " + item.name);
      ItemSlot slot = Instantiate(itemSlot, list);
      slot.Initialize(item, this);
      itemSlots.Add(slot);
    }

    public void AddItems(List<Item> items)
    {
      foreach (var item in items)
      {
        AddItem(item);
      }
    }

    public void DeleteItemSlot(ItemSlot slot)
    {
      slot.ToggleItemModifiers(false);
      itemSlots.Remove(slot);
      if (selectedSlot == slot) selectedSlot = null;
      Destroy(slot.gameObject);
    }

    public void DeleteItem(Item item)
    {
      ItemSlot slot = itemSlots.Find(x => x.item == item);
      if (slot)
      {
        slot.ToggleItemModifiers(false);
        itemSlots.Remove(slot);
        Destroy(slot.gameObject);
      }
    }

    public void DeleteSelectedItem()
    {
      if (!selectedSlot) return;
      DeleteItemSlot(selectedSlot);
    }

    public void DeleteAllItems()
    {
      Clear();
    }

    public override void SelectSlot(ItemSlot slot)
    {
      if (selectedSlot == slot)
      {
        onDoubleClick.Invoke(slot.item);
      }
      else if (selectedSlot) selectedSlot.Deselect();

      selectedSlot = slot;
    }

    void Clear()
    {
      foreach (var slot in itemSlots)
      {
        Destroy(slot.gameObject);
      }
      itemSlots = new List<ItemSlot>();
    }

  

    public object CaptureSaveData(SaveType saveType)
    {
      List<object> saveData = new List<object>();
      if (saveType != SaveType.All && saveType != saveRestriction) return null;

      foreach (ItemSlot slot in itemSlots)
      {
        saveData.Add(slot.item.GetSaveData());
      }
      return saveData;
    }

    public void RestoreSaveData(object data)
    {
      Clear();
      List<object> saveData = (List<object>)data;
      foreach (object obj in saveData)
      {
        AddItem((obj as Item.SaveData).CreateItemFromData());
      }
    }
  }
}