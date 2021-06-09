using UnityEngine;

namespace RPG.Interaction
{
  public class Item : MonoBehaviour
  {
    string baseResourcePath;
    public Sprite icon;
    public float weight;
    public ItemType itemType;
    public ItemStatModifier modifierType;


    [System.Serializable]
    struct SaveData
    {
      public string sprite;
      public ItemType type;
    }


    public static Item Create(GenericItem baseItem)
    {
      GameObject gameObject = new GameObject("Pickup");
      Item item = gameObject.AddComponent<Item>();
      item.Initialize(baseItem);
      return item;
    }

    public void Initialize(GenericItem baseItem)
    {
      this.icon = baseItem.icon;
      this.weight = baseItem.weight;
      this.itemType = baseItem.itemType;
      this.baseResourcePath = baseItem.baseResourcePath;
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