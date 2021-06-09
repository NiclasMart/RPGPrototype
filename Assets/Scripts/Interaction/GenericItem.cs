using UnityEngine;

namespace RPG.Interaction
{
  [CreateAssetMenu(fileName = "Item", menuName = "RPGPrototype/Item", order = 0)]
  public class GenericItem : ScriptableObject
  {
    public string baseResourcePath = "ItemSprites/";
    public Sprite icon;
    public float weight;
    public ItemType itemType;

    // string itemID = null;

    // public string ID => itemID;

    // public void CreateID()
    // {
    //   if (string.IsNullOrWhiteSpace(itemID))
    //   {
    //     itemID = System.Guid.NewGuid().ToString();
    //   }
    // }
  }
}