using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Items;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using TMPro;
using RPG.Combat;

namespace RPG.Interaction
{
  public class PlayerInventory : Inventory, ISaveable
  {
    [SerializeField] TextMeshProUGUI gemDisplay;
    Dictionary<ItemType, EquipmentSlot> equipmentDictionary = new Dictionary<ItemType, EquipmentSlot>();
    int gemCount;


    private void Awake()
    {
      InitializeEquipmentSlots();
      LoadSaveData();
      TryEquipBaseWeapon();
      HideAllInventorys();
    }

    public void TryEquipBaseWeapon()
    {
      if (equipmentDictionary[ItemType.Weapon].item == null)
      {
        equipmentDictionary[ItemType.Weapon].EquipItem(PlayerInfo.GetPlayer().GetComponent<GearChanger>().EquipDefaultWeapon());
        RecalculateModifiers();
      }
    }

    public void AddItem(Item item)
    {
      if (item == null) return;
      SimpleInventory inventory = equipmentDictionary[item.itemType].connectedInventory;
      inventory.AddItem(item);
    }

    public void AddItems(List<Item> items)
    {
      foreach (Item item in items)
      {
        AddItem(item);
      }
    }

    public SimpleInventory GetConnectedInventory(ItemType type)
    {
      return equipmentDictionary[type].connectedInventory;
    }

    public override void SelectSlot(ItemSlot slot)
    {
      EquipmentSlot equipSlot = slot as EquipmentSlot;
      if (!equipSlot) return;

      if (selectedSlot) selectedSlot.Deselect();
      selectedSlot = equipSlot;
    }

    public Item GetEquipedItem(ItemType type)
    {
      if (type == ItemType.Ability) return GetSelectedAbility();
      else if (equipmentDictionary.ContainsKey(type)) return equipmentDictionary[type].item;
      else return null;
    }

    private Item GetSelectedAbility()
    {
      if (selectedSlot == null) return null;
      return selectedSlot.item;
    }

    public void RecalculateModifiers()
    {
      ModifyTable table = new ModifyTable();
      foreach (var slot in equipmentDictionary.Values)
      {
        ModifiableItem modItem = slot.item as ModifiableItem;
        if (modItem == null) continue;

        GetModifiersFromItem(table, modItem);
      }

      PlayerInfo.GetPlayer().GetComponent<CharacterStats>().RecalculateStats(table);
    }

    private static void GetModifiersFromItem(ModifyTable table, ModifiableItem modItem)
    {
      modItem.GetStats(table);
      foreach (var modifier in modItem.modifiers)
      {
        modifier.effect.Invoke(table, modifier.value);
        modifier.legendaryInstallEffect.Invoke(modifier.value);
      }
    }

    public void RegisterDoubleClickAction(Action<Item> action)
    {
      foreach (var slot in equipmentDictionary.Values)
      {
        slot.connectedInventory.onDoubleClick += action;
      }
    }

    public void UnregisterDoubleClickAction(Action<Item> action)
    {
      foreach (var slot in equipmentDictionary.Values)
      {
        slot.connectedInventory.onDoubleClick -= action;
      }
    }

    public void AddGems(int amount)
    {
      gemCount += amount;
      gemDisplay.text = gemCount.ToString();
    }

    public int GetGems(int amount)
    {
      if (amount > gemCount) return 0;
      gemCount -= amount;
      gemDisplay.text = gemCount.ToString();
      return amount;

    }

    public List<Item> GetAllItemsByRank(Rank rank)
    {
      List<Item> list = new List<Item>();
      foreach (var slot in equipmentDictionary.Values)
      {
        list.AddRange(slot.connectedInventory.GetItemsByRank(rank));
      }
      return list;
    }

    private void LoadSaveData()
    {
      FindObjectOfType<SavingSystem>().Load("PlayerData");
    }

    private void InitializeEquipmentSlots()
    {
      foreach (var slot in transform.GetComponentsInChildren<EquipmentSlot>())
      {
        if (!equipmentDictionary.ContainsKey(slot.equipmentType)) equipmentDictionary.Add(slot.equipmentType, slot);
        
        slot.connectedInventory.InitializeColorParameters();
        slot.Initialize(null, this);
      }
    }

    public void HideAllInventorys()
    {
      foreach (var slot in GetComponentsInChildren<SimpleInventory>())
      {
        slot.transform.GetChild(0).gameObject.SetActive(false);
      }

    }

    public object CaptureSaveData(SaveType saveType)
    {
      return gemCount;
    }

    public void RestoreSaveData(object data)
    {
      gemCount = 0;
      AddGems((int)data);
    }
  }
}
