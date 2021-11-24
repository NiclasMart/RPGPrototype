using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Items
{
  public class LootGenerator : MonoBehaviour
  {
    const float _dropSpread = 0.5f;
    public static LootGenerator instance;

    [Header("Parameter")]
    [SerializeField, Range(0, 1f)] float dropChance = 0.3f;
    [SerializeField, Min(0)] int maxDropAmount = 1;
    [SerializeField, Range(0, 1)] float chanceReductionPerDrop = 0;
    [SerializeField] Pickup pickupPrefab;
    [SerializeField] ItemPool itemPool;


    private void Awake()
    {
      if (instance != null) Destroy(instance.gameObject);
      instance = this;

      itemPool.NoramlizeList();
      BuildDropProbabilityTable();
    }

    public void DropLoot(Vector3 dropPosition, float soulEnergyLevel)
    {
      List<Item> drops = GenerateLoot(soulEnergyLevel);
      EjectLoot(drops, dropPosition);
    }

    private List<Item> GenerateLoot(float soulEnergyLevel)
    {
      List<Item> drops = new List<Item>();

      if (maxDropAmount == 0) return drops;

      float dropProb = dropChance * (1 + soulEnergyLevel);
      for (int i = 0; i < maxDropAmount; i++)
      {
        if (Random.Range(0, 1f) >= dropProb) continue;

        Item item = GenerateItem(soulEnergyLevel);
        if (item != null) drops.Add(item);

        dropProb -= (chanceReductionPerDrop * dropProb);
      }
      return drops;
    }

    private Item GenerateItem(float soulEnergyLevel)
    {
      float rand = Random.Range(0, 1f);

      foreach (var drop in itemPool.items)
      {
        if (rand > drop.probability) continue;

        Item newItem;
        if (drop.item.modifiable) newItem = ModifyBaseItem(drop.item, soulEnergyLevel);
        else newItem = drop.item.GenerateItem();

        return newItem;
      }
      return null;
    }

    private void EjectLoot(List<Item> drops, Vector3 position)
    {
      foreach (var drop in drops)
      {
        Vector3 spawnPosition = LootSplitter.GetFreeGridPosition(position);
        Pickup pickup = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        pickup.Spawn(drop);
      }
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


    private Item ModifyBaseItem(GenericItem baseItem, float soulEnergyLevel)
    {
      ModifiableItem item = baseItem.GenerateItem() as ModifiableItem;
      AddBaseModifiers(baseItem, item);

      SetItemRarity(baseItem, item, soulEnergyLevel);

      return item;
    }

    private void SetItemRarity(GenericItem baseItem, ModifiableItem item, float soulEnergyLevel)
    {
      float rand = Random.Range(0, 1f);
      rand += Mathf.Lerp(0, 0.1f, soulEnergyLevel);
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
        modifier.rarity = Rank.Rare;
      }
      if (rand < rareDropLimit)
      {
        item.rarity = Rank.Rare;
        return;
      }

      //epic+
      item.AddModifier(baseItem.GetRandomModifier(Rank.Epic), baseItem.modifierQuality);
      if (rand < epicDropLimit)
      {
        item.rarity = Rank.Epic;
        return;
      }

      //unique
      if (baseItem.legendaryModifiers.Count < 1) return;
      item.AddModifier(baseItem.GetRandomModifier(Rank.Legendary), baseItem.modifierQuality);
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

        item.AddModifier(modifier, baseItem.modifierQuality);
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



