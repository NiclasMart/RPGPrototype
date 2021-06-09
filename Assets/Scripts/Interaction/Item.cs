using UnityEngine;

namespace RPG.Interaction
{
  public class Item : MonoBehaviour
  {
    string baseResourcePath = "ItemSprites/";
    public Sprite icon;
    public float weight;
    public ItemType itemType;

    [System.Serializable]
    struct SaveData
    {
      public string sprite;
      public ItemType type;
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