using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using RPG.Interaction;

namespace RPG.Items
{
  public class ItemEnchantment : MonoBehaviour
  {
    [SerializeField] List<TextMeshProUGUI> modiefierSlots = new List<TextMeshProUGUI>();
    [SerializeField] TextMeshProUGUI rerollBtn;
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] PlayerInventory playerInventory;

    private void Awake()
    {
      ResetModifierSlots();
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

      for (int i = 0; i < modiefierSlots.Count; i++)
      {
        if (mItem.modifiers.Count > i)
        {
          ModifiableItem.Modifier mod = mItem.modifiers[i];
          modiefierSlots[i].text = mod.GetDisplayText();
          modiefierSlots[i].color = ModifiableItem.GetRarityColor(mod.rarity);
        }
        else
        {
          modiefierSlots[i].text = "---";
          modiefierSlots[i].color = Color.white;
        }
      }
    }

    public void RemoveItem()
    {
      playerInventory.AddItem(itemSlot.item);
      itemSlot.Initialize(null, null);
      ResetModifierSlots();
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
      foreach (var slot in modiefierSlots)
      {
        slot.text = "---";
        slot.color = Color.white;
      }
      rerollBtn.text = "Reroll for 0";
      itemSlot.Initialize(null, null);
    }
  }
}
