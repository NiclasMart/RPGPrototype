using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using UnityEngine;

namespace RPG.Interaction
{
  public class PlayerInventory : MonoBehaviour
  {
    [SerializeField] InventoryDisplay display;
    List<Pickup> items = new List<Pickup>();

    public void AddItem(Pickup item)
    {
      items.Add(item);
      display.AddNewItemToDisplay(item.icon);
    }
  }
}
