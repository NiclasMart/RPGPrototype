using System;
using RPG.Display;
using UnityEngine;
using UnityEngine.UI;
using RPG.Items;
using TMPro;
using System.Collections.Generic;

namespace RPG.Interaction
{
  public class ItemSlot : MonoBehaviour
  {
    [SerializeField] Image iconSlot;
    [SerializeField] Button selectable;
    [SerializeField] TextMeshProUGUI titleText, mainStatText, sideStatsText;
    [HideInInspector] public Item item { get; private set; }
    protected Inventory inventory;
    Color defaultColor;
    ModifierDisplay modifierDisplay;


    private void Awake()
    {
      defaultColor = selectable.GetComponent<Image>().color;
      modifierDisplay = FindObjectOfType<ModifierDisplay>();
    }

    public void Initialize(Item item, Inventory inventory)
    {
      this.item = item;
      this.inventory = inventory;
      SetIcon(item);
      SetText(item);
    }

    public virtual void Select()
    {
      //handle second click
      if (inventory.selectedSlot == this)
      {
        SimpleInventory simpleInventory = inventory as SimpleInventory;
        if (simpleInventory) simpleInventory.onSecondClick.Invoke(inventory.selectedSlot.item);
        return;
      }

      SetColor(selectable.colors.highlightedColor);
      inventory.SelectSlot(this);
    }

    public virtual void Deselect()
    {
      SetColor(defaultColor);
    }

    public void ToggleItemModifiers(bool show)
    {
      if (show) modifierDisplay.ShowModifiers(item);
      else modifierDisplay.HideModifiers();
    }

    protected void SetIcon(Item item)
    {
      if (item == null) return;
      iconSlot.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
      iconSlot.color = inventory.GetRarityColor(item.rarity);
    }

    void SetText(Item item)
    {
      if (!titleText) return;

      titleText.text = item.GetTitleText();
      mainStatText.text = item.GetMainStatText();
      sideStatsText.text = item.GetSideStatText();
    }

    void SetColor(Color color)
    {
      selectable.GetComponent<Image>().color = color;
    }
  }
}
