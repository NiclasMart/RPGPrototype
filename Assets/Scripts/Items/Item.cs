using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [Serializable]
  public class Item
  {
    public string itemID;
    public string name;
    public Rank rarity;
    public Sprite icon;
    public GameObject itemObject;
    public float weight;
    public ItemType itemType;


    [Serializable]
    public class SaveData
    {
      public string itemID;
      public Rank rarity;

      public SaveData(Item item)
      {
        itemID = item.itemID;
        rarity = item.rarity;
      }

      public virtual Item CreateItemFromData()
      {
        Item item = GenericItem.GetFromID(itemID).GenerateItem();
        item.itemID = itemID;
        item.rarity = rarity;
        return item;
      }
    }

    public Item(GenericItem baseItem)
    {
      this.itemID = baseItem.itemID;
      this.name = baseItem.name;
      this.icon = baseItem.icon;
      this.itemObject = baseItem.itemObject;
      this.weight = baseItem.weight;
      this.itemType = baseItem.itemType;
    }

    public virtual string GetTitleText()
    {
      return name;
    }

    public virtual string GetMainStatText()
    {
      return "";
    }

    public virtual object GetSaveData()
    {
      return new SaveData(this);
    }
  }
}