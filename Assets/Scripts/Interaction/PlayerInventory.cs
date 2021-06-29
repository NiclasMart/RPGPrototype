using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Items;
using RPG.Stats;
using UnityEngine;

namespace RPG.Interaction
{
  public class PlayerInventory : Inventory
  {
    Dictionary<ItemType, EquipmentSlot> equipmentDictionary = new Dictionary<ItemType, EquipmentSlot>();

    private void Awake()
    {
      Initialize();
    }

    public void AddItem(Item item)
    {
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

    public override void SelectSlot(ItemSlot slot)
    {
      EquipmentSlot equipSlot = slot as EquipmentSlot;
      if (!equipSlot) return;

      if (selectedSlot) selectedSlot.Deselect();
      selectedSlot = equipSlot;
    }

    public Item GetEquipedItem(ItemType type)
    {
      if (equipmentDictionary.ContainsKey(type)) return equipmentDictionary[type].item;
      else return null;
    }

    public void RecalculateModifiers()
    {
      ModifyTable table = new ModifyTable();
      foreach (var slot in transform.GetComponentsInChildren<EquipmentSlot>())
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
      }
    }

    private void Initialize()
    {
      foreach (var slot in transform.GetComponentsInChildren<EquipmentSlot>())
      {
        //initialize inventorys
        InitializeInventory(slot.connectedInventory);
        equipmentDictionary.Add(slot.equipmentType, slot);

        //initialize slots
        slot.Initialize(null, this);
        slot.connectedInventory.gameObject.SetActive(false);
      }

      //equip default weapon
      GenericItem defaultWeapon = PlayerInfo.GetPlayer().GetComponent<GearChanger>().GetDefaultWeapon();
      if (defaultWeapon) equipmentDictionary[ItemType.Weapon].SetGear(defaultWeapon.GenerateItem());
      else RecalculateModifiers();
    }

    private static void InitializeInventory(Inventory inventory)
    {
      inventory.InitializeColorParameters();
      inventory.gameObject.SetActive(false);
    }
  }
}
