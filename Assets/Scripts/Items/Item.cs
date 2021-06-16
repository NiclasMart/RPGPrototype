using UnityEngine;

namespace RPG.Items
{
  public class Item
  {
    string name;
    public Rank rarity;
    string baseResourcePath;
    public Sprite icon;
    public GameObject itemObject;
    public float weight;
    public ItemType itemType;


    [System.Serializable]
    struct SaveData
    {
      public string sprite;
      public ItemType type;
    }

    public Item(GenericItem baseItem)
    {
      this.name = baseItem.name;
      this.icon = baseItem.icon;
      this.weight = baseItem.weight;
      this.itemType = baseItem.itemType;
      this.baseResourcePath = baseItem.baseResourcePath;
      this.itemObject = baseItem.itemObject;
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
      data.sprite = baseResourcePath + icon.name;
      data.type = itemType;
      return data;
    }

    public static void CreateItemFromData(object data)
    {
      SaveData saveData = (SaveData)data;
    }
  }
}