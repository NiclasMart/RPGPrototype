using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.Items
{
  [CreateAssetMenu(fileName = "LootBank", menuName = "RPGPrototype/LootBank", order = 0)]
  public class LootBank : ScriptableObject
  {
    [SerializeField] List<Item> savedItems = new List<Item>();

    public bool Empty => savedItems.Count == 0;
    public void AddLoot(List<Item> newItems)
    {
      savedItems.AddRange(newItems);
    }

    public void AddLoot(Item newItem)
    {
      savedItems.Add(newItem);
    }

    public void ClearLoot()
    {
      savedItems = new List<Item>();
    }

    public List<Item> GetLoot()
    {
      List<Item> tmpList = savedItems;
      savedItems = new List<Item>();
      return tmpList;
    }

    public List<Item> ShowLoot()
    {
      return savedItems;
    }
  }
}

