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
    [SerializeField] TextMeshProUGUI titleText, mainStatText, valueText;
    [HideInInspector] public Item item { get; protected set; }
    protected Inventory inventory;
    Color defaultColor;
    ModifierDisplay modifierDisplay;

    public virtual void Initialize(Item item, Inventory inventory)
    {
      defaultColor = selectable.GetComponent<Image>().color;
      modifierDisplay = FindObjectOfType<ModifierDisplay>();

      this.item = item;
      this.inventory = inventory;
      SetIcon(item);
      SetText(item);
    }

    public virtual void Select()
    {
      SetColor(selectable.colors.highlightedColor);
      inventory.SelectSlot(this);
    }

    public virtual void Deselect()
    {
      SetColor(defaultColor);
    }

    public void ToggleItemModifiers(bool show)
    {
      if (item == null) return;

      if (show) modifierDisplay.ShowModifiers(item, GetComponent<RectTransform>());
      else modifierDisplay.HideModifiers();
    }

    public void DeleteItem()
    {
      (inventory as SimpleInventory).DeleteItemSlot(this);
    }

    public virtual void HandleRightClick()
    {
      SimpleInventory sInventory = inventory as SimpleInventory;
      if (sInventory) sInventory.onRightClick.Invoke(this);
    }

    protected void SetIcon(Item item)
    {
      if (item == null)
      {
        iconSlot.transform.GetChild(0).GetComponent<Image>().sprite = null;
        return;
      }

      iconSlot.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
      iconSlot.color = ModifiableItem.GetRarityColor(item.rarity);
    }

    void SetText(Item item)
    {
      if (!titleText) return;

      titleText.text = item.GetTitleText();
      mainStatText.text = item.GetMainStatText();
      valueText.text = item.GetSellValue().ToString();
    }

    void SetColor(Color color)
    {
      selectable.GetComponent<Image>().color = color;
    }
  }
}
