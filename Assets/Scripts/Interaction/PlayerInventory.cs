using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using UnityEngine;

namespace RPG.Interaction
{
  public class PlayerInventory : MonoBehaviour
  {
    [SerializeField] InventoryDisplay display;
    [SerializeField] float capacity = 100f;
    float currentCapacity;
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
      items.Add(item);
      RecalculateCapacity(item.weight);
      display.AddNewItemToDisplay(item.icon, item.ID);
    }

    public bool CheckCapacity(float weight)
    {
      return currentCapacity + weight <= capacity;
    }

    public void DeleteItem()
    {
      print("delete");
      string itemId = display.currentlySelectedSlot.slottedItemID;
      foreach (var item in items)
      {
        if (item.ID != itemId) continue;
        items.Remove(item);
        break;
      }
      display.DeleteSelectedItem();
    }

    void RecalculateCapacity(float weight)
    {
      currentCapacity += weight;
      display.UpdateCapacityDisplay(currentCapacity, capacity);
    }
  }
}
