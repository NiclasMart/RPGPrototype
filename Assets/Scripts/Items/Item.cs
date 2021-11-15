using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [Serializable]
  public class Item
  {
    string itemID;
    string name;
    public Rank rarity;
    public Sprite icon;
    public GameObject itemObject;
    public float weight;
    public ItemType itemType;


    [Serializable]
    struct SaveData
    {
      public string itemID;
      public Rank rarity;
      public List<ModifiableItem.SerializableModifier> modifiers;
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

    public object GetSaveData()
    {
      SaveData data = new SaveData();
      data.itemID = itemID;
      data.rarity = rarity;
      //if (this is ModifiableItem) data.modifiers = new List<string>();
      if (this is ModifiableItem) data.modifiers = (this as ModifiableItem).GetSerializableModifiers();
      return data;
    }

    public static Item CreateItemFromData(object data)
    {
      SaveData saveData = (SaveData)data;
      Item item = GenericItem.GetFromID(saveData.itemID).GenerateItem();
      if (saveData.modifiers != null) (item as ModifiableItem).DeserializeModifiers((List<ModifiableItem.SerializableModifier>)saveData.modifiers);
      item.rarity = saveData.rarity;
      return item;
    }
  }
}