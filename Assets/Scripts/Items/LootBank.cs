using System.Collections;
using System.Collections.Generic;
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
    public List<Item> GetLoot()
    {
      List<Item> tmpList = savedItems;
      //savedItems = null;
      return tmpList;
    }
  }
}

