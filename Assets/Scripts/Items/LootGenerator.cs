using System.Collections.Generic;
using RPG.Core;
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
      BuildDropProbabilityTable();
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
        Vector3 spawnPosition = LootSplitter.GetFreeGridPosition(transform.position);
        Pickup pickup = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        pickup.Spawn(drop);
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

    float normalDropLimit, rareDropLimit, epicDropLimit, modifierProbability;
    private void BuildDropProbabilityTable()
    {
      GlobalParameters param = PlayerInfo.GetGlobalParameters();
      normalDropLimit = param.normalDropRate;
      rareDropLimit = normalDropLimit + param.rareDropRate;
      epicDropLimit = rareDropLimit + param.epicDropRate;

      modifierProbability = param.modifierProbability;
    }


    private Item ModifyBaseItem(GenericItem baseItem)
    {
      ModifiableItem item = baseItem.GenerateItem() as ModifiableItem;
      AddBaseModifiers(baseItem, item);

      //SetRarity()
      SetItemRarity(baseItem, item);

      return item;
    }

    private void SetItemRarity(GenericItem baseItem, ModifiableItem item)
    {
      float rand = Random.Range(0, 1f);
      //normal 
      if (rand < normalDropLimit)
      {
        item.rarity = Rank.Normal;
        return;
      }

      //rare+
      foreach (var modifier in item.modifiers)
      {
        modifier.value *= 1 + PlayerInfo.GetGlobalParameters().rareValueImprovement;
      }
      if (rand < rareDropLimit)
      {
        item.rarity = Rank.Rare;
        return;
      }

      //epic+
      item.AddModifier(baseItem.epicModifiers[Random.Range(0, baseItem.epicModifiers.Count)]);
      if (rand < epicDropLimit)
      {
        item.rarity = Rank.Epic;
        return;
      }

      //unique
      item.AddModifier(baseItem.legendaryModifiers[Random.Range(0, baseItem.legendaryModifiers.Count)]);
      item.rarity = Rank.Legendary;
    }

    private void AddBaseModifiers(GenericItem baseItem, ModifiableItem item)
    {
      int modifierCount = CalculateAmountOfModifiers();
      List<int> indexCache = new List<int>();
      for (int i = 0; i < modifierCount; i++)
      {
        int index;
        do index = Random.Range(0, baseItem.normalModifiers.Count);
        while (indexCache.Contains(index));
        indexCache.Add(index);
        ItemStatModifier modifier = baseItem.normalModifiers[index];

        item.AddModifier(modifier);
      }
    }

    private int CalculateAmountOfModifiers()
    {
      float rand = Random.Range(0, 1f);

      if (rand < (modifierProbability * modifierProbability)) return 3;
      if (rand < modifierProbability) return 2;
      return 1;
    }
  }
}



