using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
  public class Receiver : MonoBehaviour
  {
    SimpleInventory storage;
    void Awake()
    {
      storage = GetComponent<UIActivator>().connectedUI.GetComponent<SimpleInventory>();
    }

    public void TakeAllItems()
    {
      PlayerInventory inventory = GetComponent<UIActivator>().Interacter.GetComponent<Interacter>().mainInventory;
      inventory.AddItems(storage.GetItemList());
      storage.DeleteAllItems();
    }
  }
}
