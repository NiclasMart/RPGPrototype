using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [CreateAssetMenu(fileName = "Item", menuName = "Items/Create New GenericItem", order = 0)]
  public abstract class GenericItem : ScriptableObject
  {
    [ScriptableObjectId] public string itemID = null;
    public string name;
    public Sprite icon;
    public GameObject itemObject;
    public float weight;
    public float baseValue;
    public ItemType itemType;
    public bool modifiable = false;
    public List<ItemStatModifier> normalModifiers = new List<ItemStatModifier>();
    public List<ItemStatModifier> epicModifiers = new List<ItemStatModifier>();
    public List<ItemStatModifier> legendaryModifiers = new List<ItemStatModifier>();

    public virtual Item GenerateItem()
    {
      return new Item(this);
    }

    static Dictionary<string, GenericItem> itemLookupCache;
    public static GenericItem GetFromID(string itemID)
    {
      if (itemLookupCache == null)
      {
        itemLookupCache = new Dictionary<string, GenericItem>();
        var itemList = Resources.LoadAll<GenericItem>("");
        foreach (var item in itemList)
        {
          if (item.itemID == null) continue;
          if (itemLookupCache.ContainsKey(item.itemID))
          {
            Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
            continue;
          }

          itemLookupCache[item.itemID] = item;
        }
      }

      if (itemID == null || !itemLookupCache.ContainsKey(itemID))
      {
        Debug.Log("Found no item with ID: " + itemID);
        return null;
      }
      return itemLookupCache[itemID];
    }
  }
}