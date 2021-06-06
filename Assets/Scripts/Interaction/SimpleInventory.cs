using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace RPG.Interaction
{
  public class SimpleInventory : Inventory
  {
    [SerializeField] float capacity = 100f;

    [Header("UI Specifications")]
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] RectTransform list;
    [SerializeField] TextMeshProUGUI capacityDisplay;

    float currentCapacity;
    
    [HideInInspector] public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public List<Item> GetItemList()
    {
      List<Item> itemList = new List<Item>();
      foreach (ItemSlot slot in itemSlots)
      {
        itemList.Add(slot.item);
      }
      return itemList;
    }

    public void AddItem(Item item)
    {
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

    public void DeleteSelectedItem()
    {
      if (!selectedSlot) return;
      itemSlots.Remove(selectedSlot);
      Destroy(selectedSlot.gameObject);
      selectedSlot = null;
    }

    public void DeleteAllItems()
    {
      itemSlots = new List<ItemSlot>();
      Clear();
    }

    public override void SelectSlot(ItemSlot slot)
    {
      if (selectedSlot) selectedSlot.Deselect();
      selectedSlot = slot;
    }

    public bool CheckCapacity(float weight)
    {
      return currentCapacity + weight <= capacity;
    }

    void Clear()
    {
      foreach (Transform slot in list.transform)
      {
        if (slot == transform) continue;
        Destroy(slot.gameObject);
      }
    }

    void RecalculateCapacity(float weight)
    {
      currentCapacity += weight;
      UpdateCapacityDisplay(currentCapacity, capacity);
    }

    void UpdateCapacityDisplay(float currentValue, float maxValue)
    {
      if (!capacityDisplay) return;
      string value = string.Concat(currentValue + "/" + maxValue);
      capacityDisplay.text = value;
    }
  }
}