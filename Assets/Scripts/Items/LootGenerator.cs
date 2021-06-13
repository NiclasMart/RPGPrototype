using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  public class LootGenerator : MonoBehaviour
  {
    const float _dropSpread = 0.5f;

    [Header("Parameter")]
    [SerializeField, Range(0, 1f)] float dropChance = 0.3f;
    [SerializeField, Min(0)] int maxDropAmount = 1;
    [SerializeField, Range(0, 1)] float chanceReductionPerDrop = 0;

    [SerializeField] Pickup pickupPrefab;

    [SerializeField] List<Drop> itemPool = new List<Drop>();

    [System.Serializable]
    class Drop
    {
      public GenericItem item;
      public float probability;
    }

    private void Awake()
    {
      NoramlizeList();
    }

    public void DropLoot()
    {
      List<Item> drops = GenerateLoot();
      EjectLoot(drops);
    }

    private void NoramlizeList()
    {
      float totalProbability = 0;
      foreach (var drop in itemPool)
      {
        totalProbability += drop.probability;
      }

      float cumulatedProbability = 0;
      foreach (var drop in itemPool)
      {
        drop.probability /= totalProbability;
        float normalizedProb = drop.probability;
        drop.probability += cumulatedProbability;
        cumulatedProbability += normalizedProb;
      }

      itemPool.Sort((x, y) => x.probability.CompareTo(y.probability));
    }

    private void EjectLoot(List<Item> drops)
    {
      foreach (var drop in drops)
      {
        Pickup pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        pickup.Initialize(drop);
      }
    }

    private List<Item> GenerateLoot()
    {
      List<Item> drops = new List<Item>();

      if (maxDropAmount == 0) return drops;

      float dropProb = dropChance;
      for (int i = 0; i < maxDropAmount; i++)
      {
        if (Random.Range(0, 1f) >= dropProb) continue;

        Item item = GenerateItem();
        if (item != null) drops.Add(item);

        dropProb -= (chanceReductionPerDrop * dropProb);
      }
      return drops;
    }

    private Item GenerateItem()
    {
      float rand = Random.Range(0, 1f);

      foreach (var drop in itemPool)
      {
        if (rand > drop.probability) continue;

        Item newItem;
        if (drop.item.modifiable) newItem = ModifyBaseItem(drop.item);
        else newItem = drop.item.GenerateItem();

        return newItem;
      }
      return null;
    }


    private Item ModifyBaseItem(GenericItem baseItem)
    {
      ModifiableItem item = baseItem.GenerateItem() as ModifiableItem;
      ItemStatModifier modifier = baseItem.modifiers[0];
      item.AddModifier(modifier);
      return item;
    }
  }
}



