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

    public int RecycleValue
    {
      get { return recycleValue; }
      set
      {
        recycleValue = value;
        UpdateValueDisplay();
      }
    }

    private void Awake()
    {
      list.onDoubleClick += RemoveItemFromRecycler;
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

    public void AddItem(Item item)
    {
      list.AddItem(item);
      playerInventory.GetConnectedInventory(item.itemType).DeleteItem(item);
      RecycleValue += item.GetSellValue();
      UpdateValueDisplay();
    }

    public void RemoveAllItems()
    {
      foreach (var item in list.GetItemList())
      {
        RemoveItemFromRecycler(item);
      }
      RecycleValue = 0;
    }

    public void Recycle()
    {
      playerInventory.AddGems(RecycleValue);
      RecycleValue = 0;
      list.DeleteAllItems();
    }

    void RemoveItemFromRecycler(Item item)
    {
      list.DeleteItem(item);
      playerInventory.AddItem(item);
      RecycleValue -= item.GetSellValue();
    }

    public void GetAllItemsByRank(int rank)
    {
      List<Item> items = playerInventory.GetAllItemsByRank((Rank)rank);

      foreach (var item in items)
      {
        AddItem(item);
      }
    }

    void UpdateValueDisplay()
    {
      valueDisplay.text = (RecycleValue != 1) ? RecycleValue.ToString() + " Gems" : RecycleValue.ToString() + " Gem";
    }
  }
}