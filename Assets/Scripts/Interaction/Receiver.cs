using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using UnityEngine;

namespace RPG.Interaction
{
  public class Receiver : MonoBehaviour
  {
    SimpleInventory storage;
    void Awake()
    {
      storage = GetComponent<UIActivator>().connectedUI.GetComponent<SimpleInventory>();
      //storage.onRightClick += TakeSelectedItem;
    }

    public void TakeAllItems()
    {
      PlayerInventory inventory = GetComponent<UIActivator>().Interacter.GetComponent<Interacter>().mainInventory;
      inventory.AddItems(storage.GetItemList());
      storage.DeleteAllItems();
    }

    public void TakeSelectedItem(Item selectedItem)
    {
      PlayerInventory inventory = GetComponent<UIActivator>().Interacter.GetComponent<Interacter>().mainInventory;
      inventory.AddItem(selectedItem);
      storage.DeleteSelectedItem();
    }
  }
}
