using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using RPG.Interaction;
using RPG.Saving;
using UnityEngine.UI;
using RPG.Core;

namespace RPG.Items
{
  public class ItemEnchantment : MonoBehaviour, ISaveable
  {
    [SerializeField] List<EnchantmentModifierSlot> modifierSlot = new List<EnchantmentModifierSlot>();
    [SerializeField] TextMeshProUGUI rerollBtn;
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] List<int> rerollPrices;
    [SerializeField] Color selectColor;
    EnchantmentModifierSlot previouseSelectedSlot = null;

    private void Awake()
    {
      foreach (var slot in modifierSlot)
      {
        slot.selectColor = selectColor;
      }
    }


    public void AddItem(Item item)
    {
      if (item != null && itemSlot.item != null)
      {
        playerInventory.AddItem(itemSlot.item);
      }
      itemSlot.Initialize(item, null);
      LoadModifiers(item);
      playerInventory.GetConnectedInventory(item.itemType).DeleteItem(item);
    }

    private void LoadModifiers(Item item)
    {
      ModifiableItem mItem = item as ModifiableItem;

      for (int i = 0; i < modifierSlot.Count; i++)
      {
        if (mItem.modifiers.Count > i)
        {
          ModifiableItem.Modifier mod = mItem.modifiers[i];
          modifierSlot[i].SetModifier(mItem.modifiers[i]);
        }
        else
        {
          modifierSlot[i].ResetSlot();
        }
      }
    }

    public void RemoveItem()
    {
      playerInventory.AddItem(itemSlot.item);
      itemSlot.Initialize(null, null);
      ResetModifierSlots();
      previouseSelectedSlot = null;
    }

    public void SelectModifier(EnchantmentModifierSlot slot)
    {
      HandleSelectionColor(slot);

      ModifiableItem.Modifier mod = slot.GetModifier();

      if (mod == null)
      {
        rerollBtn.text = "Reroll for 0";
        return;
      }
      if (itemSlot.item.rarity == Rank.Rare && mod.rarity == Rank.Normal) rerollBtn.text = "Reroll for " + rerollPrices[1];
      else rerollBtn.text = "Reroll for " + rerollPrices[(int)mod.rarity];
    }

    private void HandleSelectionColor(EnchantmentModifierSlot slot)
    {
      if (previouseSelectedSlot != null) previouseSelectedSlot.Deselect();
      slot.Select();
      previouseSelectedSlot = slot;
    }

    public void Reroll()
    {
      if (previouseSelectedSlot == null || previouseSelectedSlot.GetModifier() == null) return;

      //get mod and pay price
      ModifiableItem.Modifier mod = previouseSelectedSlot.GetModifier();
      int price = (itemSlot.item.rarity == Rank.Rare && mod.rarity == Rank.Normal) ? rerollPrices[1] : rerollPrices[(int)mod.rarity];
      int payed = playerInventory.GetGems(price);
      if (payed == 0) return;

      int index = (itemSlot.item as ModifiableItem).modifiers.IndexOf(mod);
      (itemSlot.item as ModifiableItem).modifiers.Remove(mod);

      //get baseModifier
      GenericItem baseItem = GenericItem.GetFromID(itemSlot.item.itemID);
      ItemStatModifier baseMod;
      do baseMod = baseItem.GetRandomModifier(mod.rarity);
      while ((itemSlot.item as ModifiableItem).modifiers.Find(x => x.name == baseMod.name) != null);

      ModifiableItem.Modifier newMod = new ModifiableItem.Modifier(baseMod, baseItem.modifierQuality);

      //upgrade modifier if rerolled modifier was rare
      if (itemSlot.item.rarity == Rank.Rare && mod.rarity == Rank.Normal)
        newMod.value *= 1 + PlayerInfo.GetGlobalParameters().rareValueImprovement;

      

      (itemSlot.item as ModifiableItem).modifiers.Insert(index, newMod);
      LoadModifiers(itemSlot.item);
      FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.All);
    }

    public void HandleOpening()
    {
      playerInventory.RegisterDoubleClickAction(AddItem);
    }

    public void HandleClosing()
    {
      playerInventory.UnregisterDoubleClickAction(AddItem);
    }

    private void ResetModifierSlots()
    {
      foreach (var slot in modifierSlot)
      {
        slot.ResetSlot();
      }
      rerollBtn.text = "Reroll for 0";
      itemSlot.Initialize(null, null);
    }

    public object CaptureSaveData(SaveType saveType)
    {
      return (itemSlot.item != null) ? itemSlot.item.GetSaveData() : null;
    }

    public void RestoreSaveData(object data)
    {
      AddItem((data as Item.SaveData).CreateItemFromData());
    }
  }
}
