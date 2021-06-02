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
    List<Pickup> items = new List<Pickup>();

    public void AddItem(Pickup item)
    {
      items.Add(item);
      RecalculateCapacity(item.weight);
      display.AddNewItemToDisplay(item.icon);
    }

    public bool CheckCapacity(float weight)
    {
      return currentCapacity + weight <= capacity;
    }

    void RecalculateCapacity(float weight)
    {
      currentCapacity += weight;
      display.UpdateCapacityDisplay(currentCapacity, capacity);
    }
  }
}
