using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [CreateAssetMenu(fileName = "LootBank", menuName = "RPGPrototype/LootBank", order = 0)]
  public class LootBank : ScriptableObject
  {
    [SerializeField] List<Item> savedItems = new List<Item>();

    public void AddLoot(List<Item> newItems)
    {
      savedItems.AddRange(newItems);
    }
  }
}

