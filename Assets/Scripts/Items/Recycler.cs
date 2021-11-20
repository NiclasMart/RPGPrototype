using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Core;
using RPG.Items;

namespace RPG.Interaction
{
  public class Recycler : MonoBehaviour
  {
    [SerializeField] TextMeshProUGUI valueDisplay;
    [SerializeField] SimpleInventory list;
    [SerializeField] PlayerInventory playerInventory;
    int recycleValue = 0;

    private void Awake()
    {
      list.onDoubleClick += RemoveItemFromRecycle;
    }

    public void HandleOpening()
    {
      playerInventory.RegisterDoubleClickAction(AddItem);
    }

    public void HandleClosing()
    {
      playerInventory.UnregisterDoubleClickAction(AddItem);
      Recycle();
    }

    public void AddItem(Item item, SimpleInventory caller)
    {
      list.AddItem(item);
      caller.DeleteItem(item);
      recycleValue += item.GetSellValue();
      UpdateValueDisplay();


    }

    public void Recycle()
    {
      playerInventory.AddGems(recycleValue);
      recycleValue = 0;
      UpdateValueDisplay();
      list.DeleteAllItems();
    }

    void RemoveItemFromRecycle(Item item, SimpleInventory caller)
    {
      list.DeleteItem(item);
      playerInventory.AddItem(item);

    }

    void UpdateValueDisplay()
    {
      valueDisplay.text = (recycleValue != 1) ? recycleValue.ToString() + " Gems" : recycleValue.ToString() + " Gem";
    }
  }
}