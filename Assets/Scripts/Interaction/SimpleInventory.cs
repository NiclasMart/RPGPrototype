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
    [SerializeField] float capacity = 100f;

    [Header("UI Specifications")]
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] RectTransform list;
    [SerializeField] TextMeshProUGUI capacityDisplay;

    float currentCapacity;
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
      RecalculateCapacity(item.weight);
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
      RecalculateCapacity(-slot.item.weight);
      slot.ToggleItemModifiers(false);
      itemSlots.Remove(slot);
      if (selectedSlot == slot) selectedSlot = null;
      Destroy(slot.gameObject);
    }

    public void DeleteItem(Item item)
    {
      RecalculateCapacity(-item.weight);
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

    public bool CheckCapacity(float weight)
    {
      return currentCapacity + weight <= capacity;
    }

    void Clear()
    {
      foreach (var slot in itemSlots)
      {
        Destroy(slot.gameObject);
        //??? itemSlots.Remove(selectedSlot);
      }
      itemSlots = new List<ItemSlot>();
      ResetCapacity();
    }

    void RecalculateCapacity(float weight)
    {
      currentCapacity += weight;
      UpdateCapacityDisplay(currentCapacity, capacity);
    }

    void ResetCapacity()
    {
      currentCapacity = 0;
      UpdateCapacityDisplay(currentCapacity, capacity);
    }

    void UpdateCapacityDisplay(float currentValue, float maxValue)
    {
      if (!capacityDisplay) return;
      string value = string.Concat(currentValue + "/" + maxValue);
      capacityDisplay.text = value;
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