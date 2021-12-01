using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [System.Serializable]
  public class Drop
  {
    public Drop(GenericItem item, float prob)
    {
      this.item = item;
      this.probability = prob;
    }

    public GenericItem item;
    public float probability;
  }

  [CreateAssetMenu(fileName = "ItemPool", menuName = "Items/Create New Item Pool", order = 0)]
  public class ItemPool : ScriptableObject
  {
    public List<Drop> drops = new List<Drop>();
    [HideInInspector] public List<Drop> items;

    public void NoramlizeList()
    {
      items = new List<Drop>();
      float totalProbability = 0;

      foreach (var drop in drops)
      {
        totalProbability += drop.probability;
      }

      float cumulatedProbability = 0;
      foreach (var drop in drops)
      {
        float prob = drop.probability;
        prob /= totalProbability;
        Drop newItem = new Drop(drop.item, prob + cumulatedProbability);
        cumulatedProbability += prob;

        items.Add(newItem);
      }

      items.Sort((x, y) => x.probability.CompareTo(y.probability));
    }
  }
}
