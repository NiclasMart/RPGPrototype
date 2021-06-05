using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
  public class Receiver : MonoBehaviour
  {
    Inventory storage;
    void Awake()
    {
      storage = GetComponent<UIActivator>().connectedUI.GetComponent<Inventory>();
    }

    public void TakeAllItems()
    {
      Inventory inventory = GetComponent<UIActivator>().Interacter.GetComponent<Interacter>().mainInventory;
      inventory.AddItems(storage.GetItemList());
      storage.DeleteAllItems();
    }
  }
}
