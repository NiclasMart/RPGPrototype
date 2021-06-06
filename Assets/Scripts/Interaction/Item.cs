using UnityEngine;

namespace RPG.Interaction
{
  [CreateAssetMenu(fileName = "Item", menuName = "RPGPrototype/Item", order = 0)]
  public class Item : ScriptableObject
  {
    string itemID = null;
    public Sprite icon;
    public float weight;
    public ItemType itemType;

    public string ID => itemID;

    public void CreateID()
    {
      if (string.IsNullOrWhiteSpace(itemID))
      {
        itemID = System.Guid.NewGuid().ToString();
      }
    }
  }
}