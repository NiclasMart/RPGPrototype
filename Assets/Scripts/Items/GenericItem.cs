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
    [Range(0, 1)] public float modifierQuality;
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

    protected float GetRandomValueByQuality(float min, float max)
    {
      float lowQuality, highQuality, lokalMin, lokalMax;
      lowQuality = Mathf.Max(0, modifierQuality - 0.1f);
      highQuality = Mathf.Min(1, modifierQuality + 0.1f);
      lokalMin = Mathf.Lerp(min, max, lowQuality);
      lokalMax = Mathf.Lerp(min, max, highQuality);
      return UnityEngine.Random.Range(lokalMin, lokalMax);
    }

    public ItemStatModifier GetRandomModifier(Rank rarity)
    {
      switch (rarity)
      {
        case Rank.Normal:
        case Rank.Rare:
          return normalModifiers[UnityEngine.Random.Range(0, normalModifiers.Count)];
        case Rank.Epic:
          return epicModifiers[UnityEngine.Random.Range(0, epicModifiers.Count)];
        case Rank.Legendary:
          return legendaryModifiers[UnityEngine.Random.Range(0, legendaryModifiers.Count)];
      }
      return null;
    }
  }
}